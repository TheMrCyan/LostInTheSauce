using System.Collections.Generic;
using UnityEngine;

public class S_T_Fridge : MonoBehaviour
{
    public static S_T_Fridge Instance { get; private set; }

    private bool touchingPlayer;
    public List<int> fridgeContents;

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
                for (int j = 0; j < fridgeContents.Count; j++)
                {
                    if (burgerRecipe[i] == fridgeContents[j])
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

            // Store held item
            if (S_T_PlayerMovement.Instance.heldItem.sprite != null)
            {

                var id = 0;
                // Return ingredients id
                for (int i = 0; i < S_T_ItemManager.Instance.ingredients.Length; i++)
                {
                    if (S_T_PlayerMovement.Instance.heldItem.sprite == S_T_ItemManager.Instance.ingredients[i])
                    {
                        id = i;
                        break;
                    }
                }

                fridgeContents.Add(id);
                S_T_PlayerMovement.Instance.heldItem.sprite = null;
            }
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
