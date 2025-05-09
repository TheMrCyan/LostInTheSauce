using UnityEngine;

public class S_T_PrepTable : MonoBehaviour
{
    public static S_T_PrepTable Instance { get; private set; }

    private bool touchingPlayer;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (touchingPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            int[] burgerRecipe = { (int)Food.Meat, (int)Food.Dough, (int)Food.Tomato };
            bool foundIngredient;
            bool failedRecipe = false;
            // Try to make a burger
            // Go through fridge ingredients
            for (int i = 0; i < burgerRecipe.Length; i++)
            {
                foundIngredient = false;
                for (int j = 0; j < S_T_Fridge.Instance.fridgeContents.Count; j++)
                {
                    if (burgerRecipe[i] == S_T_Fridge.Instance.fridgeContents[j])
                    {
                        Debug.Log("Found " + burgerRecipe[i] + " in the fridge!");
                        foundIngredient = true;
                        break;
                    }
                }

                if (!foundIngredient)
                {
                    failedRecipe = true;
                }
            }

            if (!failedRecipe)
            {
                Debug.Log("Made a burger!");
            }
            else
            {
                Debug.Log("Failed to make recipe!");
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
