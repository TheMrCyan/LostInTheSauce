using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class S_T_PlayerMovement : MonoBehaviour
{
    public static S_T_PlayerMovement Instance { get; private set; }

    private bool canSprint = true;
    private bool isMoving;
    [System.NonSerialized] public bool touchingFloorItem;
    [System.NonSerialized] public bool touchingTrash;
    private float speed;
    private float stamina;
    public float totalStamina;
    private Vector2 input;
    private Rigidbody2D rb;
    public GameObject staminaBar;
    public SpriteRenderer heldItem;
    public float speedMultiplier = 2f;

    Animator anim;
    private Vector2 lastMoveDirection;
    private bool facingRight = true;

    private bool useExternalSpeed = false;
    private float externalSpeed;


    void Awake()
    {
        Instance = this;
        stamina = totalStamina;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        if (!S_T_PauseMenu.isPaused)
        {
            // Stores last move direction when player stops moving
            float MoveX = Input.GetAxisRaw("Horizontal");
            float MoveY = Input.GetAxisRaw("Vertical");

            if ((MoveX == 0 && MoveY == 0) && (input.x != 0 || input.y != 0))
            {
                lastMoveDirection = input;
            }

            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            input.Normalize(); // Makes diagonal movement the same speed as other movement

            isMoving = input.x != 0 || input.y != 0;

            anim.SetBool("isSprinting", false);

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                canSprint = true;
            }

            if (isMoving)
            {

                if (!useExternalSpeed)
                {
                    speed = 6f;
                }
                else
                {
                    speed = externalSpeed;
                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (canSprint)
                    {
                        // Sprinting
                        if (stamina > 0f)
                        {
                            stamina -= 0.2f;
                            speed = speed * speedMultiplier;
                            anim.SetBool("isSprinting", true);
                        }
                        else canSprint = false;
                    }
                    else
                    {
                        stamina += 0.1f;
                        anim.SetBool("isSprinting", false);
                    }
                }
                else
                {
                    stamina += 0.1f;
                    anim.SetBool("isSprinting", false);
                }
            }
            else
            {
                stamina += 0.3f;
                speed = 0f;
                anim.SetBool("isSprinting", false);
            }

            // Stamina stays in bounds
            stamina = Mathf.Clamp(stamina, 0f, totalStamina);

            // Adjusts the stamina bar to represent current stamina
            if (staminaBar != null)
            {
                staminaBar.transform.localScale = new Vector2(stamina / totalStamina, staminaBar.transform.localScale.y);
            }

            // Drop held item unless at the fridge or stove 
            if (Input.GetKeyDown(KeyCode.Space) && heldItem.sprite != null && !S_T_Fridge.Instance.touchingPlayer && !S_T_Stove.Instance.touchingPlayer && !touchingFloorItem)
            {
                heldItem.sprite = null;
            }

            Animate();
            if (input.x < 0 && facingRight || input.x > 0 && !facingRight)
            {
                Flip();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed * 60 * Time.fixedDeltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            touchingFloorItem = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            touchingFloorItem = false;
        }
    }

    public S_T_ItemGen GrabLinkedItem(int id)
    {
        var item = Physics2D.OverlapCircle(new Vector2(1000 + id * 2, 0), 1);
        if (item == null)
        {
            Debug.LogError($"No item found with ID {id}");
            return null;
        }

        // Add component check
        var itemGen = item.GetComponent<S_T_ItemGen>();
        if (itemGen == null)
        {
            Debug.LogError($"Item at ID {id} missing S_T_ItemGen component");
            return null;

        }
        return itemGen;
    }
    public void ResetSpeed()
    {
        useExternalSpeed = false;  
       
    }

    public void SetSpeed(float NewSpeed)
    {
        externalSpeed = NewSpeed;
        useExternalSpeed = true;
    }
    private void Animate()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flips the sprite by making x negative
        transform.localScale = scale;
        facingRight = !facingRight;
    }
}