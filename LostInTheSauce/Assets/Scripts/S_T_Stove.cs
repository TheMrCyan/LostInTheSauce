using UnityEngine;

public class S_T_Stove : MonoBehaviour
{
    public static S_T_Stove Instance { get; private set; }

    public bool touchingPlayer;
    private bool cooked;
    private int ingredientId;
    private float cookingTime;
    private float graceTimer;
    public float perfectDoneTime;
    public float graceTime;
    public SpriteRenderer heldItem;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (touchingPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            if (heldItem.sprite == null)
            {
                // Store held item
                if (S_T_PlayerMovement.Instance.heldItem.sprite != null)
                {
                    for (int i = 0; i < S_T_ItemManager.Instance.ingredients.Length; i++)
                    {
                        if (S_T_PlayerMovement.Instance.heldItem.sprite == S_T_ItemManager.Instance.ingredients[i])
                        {
                            heldItem.sprite = S_T_PlayerMovement.Instance.heldItem.sprite;
                            ingredientId = i;
                            break;
                        }
                    }

                    S_T_PlayerMovement.Instance.heldItem.sprite = null;
                    return;
                }
            }
            else // Retrieve item that's cooking
            {
                cookingTime = 0f;
                graceTimer = 0f;
                S_T_PlayerMovement.Instance.heldItem.sprite = heldItem.sprite;
                heldItem.sprite = null;
                cooked = false;
            }
        }

        // Increase time while cooking
        if (heldItem.sprite != null)
        {
            cookingTime += Time.deltaTime;

            if (cookingTime > perfectDoneTime && !cooked)
            {
                switch (ingredientId)
                {
                    case (int)Food.Dough:
                        heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.Bread];
                        break;
                    case (int)Food.Fish:
                        heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.CookedFish];
                        break;
                    case (int)Food.Meat:
                        heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.CookedMeat];
                        break;
                    case (int)Food.Egg:
                        heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.CookedEgg];
                        break;
                    case (int)Food.Cocoa:
                        heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.Chocolate];
                        break;
                    default:
                        heldItem.sprite = null;
                        break;
                }
                cooked = true;
            }

            if (cooked)
            {
                graceTimer += Time.deltaTime;
                if (graceTimer > graceTime)
                {
                    // Burn food
                    heldItem.sprite = null;
                    cooked = false;
                    graceTimer = 0f;
                    cookingTime = 0f;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
    }
}
