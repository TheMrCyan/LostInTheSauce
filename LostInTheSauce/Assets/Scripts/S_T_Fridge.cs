using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_T_Fridge : MonoBehaviour
{
    public static S_T_Fridge Instance { get; private set; }

    private bool touchingPlayer;
    public GameObject fridgeUI;
    [System.NonSerialized] public int[] fridgeContents;
    [System.NonSerialized] public List<Image> fridgeVisuals;

    private void Awake()
    {
        Instance = this;
        fridgeVisuals = new List<Image>();

        // Get all buttons inside FridgeUI
        Button[] buttons = fridgeUI.GetComponentsInChildren<Button>(true);

        foreach (Button button in buttons)
        {
            // Get every image in children of every button
            Image[] images = button.GetComponentsInChildren<Image>(true);

            foreach (Image img in images)
            {
                // Skip the background button image
                if (img == button.image)
                    continue;

                fridgeVisuals.Add(img);
            }
        }

        fridgeContents = new int[fridgeVisuals.Count];
        // Manually set all items in fridge to -1, by default it's 0 which is tomatoes
        for (int i = 0; i < fridgeContents.Length; i++)
        {
            fridgeContents[i] = -1;
        }

        fridgeUI.SetActive(false);
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

                // Find empty spot in fridge
                for (int i = 0; i < fridgeContents.Length; i++)
                {
                    if (fridgeContents[i] == -1)
                    {
                        fridgeContents[i] = id;
                        fridgeVisuals[i].sprite = S_T_PlayerMovement.Instance.heldItem.sprite;
                        break;
                    }
                }
                S_T_PlayerMovement.Instance.heldItem.sprite = null;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
            fridgeUI.SetActive(false);
        }
    }
}
