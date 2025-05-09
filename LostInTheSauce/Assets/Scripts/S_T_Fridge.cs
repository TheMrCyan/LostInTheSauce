using System.Collections.Generic;
using UnityEngine;

public class S_T_Fridge : MonoBehaviour
{
    public static S_T_Fridge Instance { get; private set; }

    private bool touchingPlayer;
    public GameObject fridgeUI;
    [System.NonSerialized] public List<int> fridgeContents;

    private void Awake()
    {
        fridgeUI.SetActive(false);
        Instance = this;
    }

    private void Update()
    {
        if (touchingPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            fridgeUI.SetActive(!fridgeUI.activeSelf);
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
