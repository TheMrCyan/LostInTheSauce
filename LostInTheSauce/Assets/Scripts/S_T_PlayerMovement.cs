using UnityEngine;

public class S_T_PlayerMovement : MonoBehaviour
{
    public static S_T_PlayerMovement Instance { get; private set; }

    private bool isMoving;
    private float speed;
    private float stamina;
    public float totalStamina;
    private Vector2 input;
    private Rigidbody2D rb;
    public GameObject staminaBar;
    public SpriteRenderer heldItem;

    void Awake()
    {
        Instance = this;
        stamina = totalStamina;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize(); // Makes diagonal movement the same speed as other movement

        isMoving = input.x != 0 || input.y != 0;

        if (isMoving)
        {
            speed = 6f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Sprinting
                if (stamina > 0f)
                {
                    stamina -= 0.2f;
                    speed = 12f;
                }
            }
            else
            {
                stamina += 0.1f;
            }
        }
        else
        {
            stamina += 0.15f;
            speed = 0f;
        }

        // Stamina stays in bounds
        stamina = Mathf.Clamp(stamina, 0f, totalStamina);

        // Adjusts the stamina bar to represent current stamina
        if (staminaBar != null)
        {
            staminaBar.transform.localScale = new Vector2(stamina / totalStamina, staminaBar.transform.localScale.y);
        }

        // Drop held item unless at the fridge or stove 
        if (Input.GetKeyDown(KeyCode.Space) && heldItem.sprite != null && !S_T_Fridge.Instance.touchingPlayer && !S_T_Stove.Instance.touchingPlayer)
        {
            heldItem.sprite = null;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed * 60 * Time.fixedDeltaTime;
    }
}
