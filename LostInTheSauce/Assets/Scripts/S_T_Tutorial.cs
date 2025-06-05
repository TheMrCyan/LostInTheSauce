using UnityEngine;
using UnityEngine.SceneManagement;

public class S_T_Tutorial : MonoBehaviour
{
    public static S_T_Tutorial Instance { get; private set; }

    public bool canEnter;
    public GameObject textBox;
    public GameObject staminaBar;
    public GameObject minimap;
    public GameObject timer;
    public GameObject prepCounter;
    public GameObject ingredientTable;
    public GameObject stove;
    public GameObject trashCan;
    public GameObject fridge;
    public GameObject cornerCounter;
    public GameObject tomato;
    public GameObject potato;
    public GameObject oil;
    public GameObject fish;
    private bool createdRecipe;
    public int currentLine;

    public bool hasPressedW;
    public bool hasPressedA;
    public bool hasPressedS;
    public bool hasPressedD;
    public bool hasPressedUp;
    public bool hasPressedDown;
    public bool hasPressedLeft;
    public bool hasPressedRight;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        staminaBar.SetActive(false);
        minimap.SetActive(false);
        timer.SetActive(false);
        ingredientTable.SetActive(false);
        stove.SetActive(false);
        trashCan.SetActive(false);
        fridge.SetActive(false);
        cornerCounter.SetActive(false);
        tomato.SetActive(false);
        potato.SetActive(false);
        oil.SetActive(false);
        fish.SetActive(false);
        canEnter = true;
        S_T_PrepTable.Instance.prepTableUI.SetActive(false);
    }

    void Update()
    {
        currentLine = S_T_TextBoxManager.Instance.currentLine;

        if (createdRecipe == false)
        {
            S_T_PrepTable.Instance.recipeBook.Clear();
            S_T_PrepTable.Instance.recipeBook.Add(new Recipe("Fish & Chips", new int[] { (int)Food.Potato, (int)Food.Oil, (int)Food.CookedFish }));
            createdRecipe = true;
            prepCounter.SetActive(false);
        }

        if (currentLine == 4)
        {
            canEnter = false;

            if (Input.GetKeyDown(KeyCode.W))
            {
                hasPressedW = true;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                hasPressedA = true;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                hasPressedS = true;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                hasPressedD = true;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                hasPressedUp = true;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                hasPressedDown = true;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                hasPressedRight = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                hasPressedLeft = true;
            }

            if ((hasPressedW && hasPressedA && hasPressedS && hasPressedD) || (hasPressedUp && hasPressedDown && hasPressedLeft && hasPressedRight))
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 5)
        {
            canEnter = false;

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 6 && !staminaBar.activeSelf)
        {
            staminaBar.SetActive(true);
        }

        if (currentLine == 7)
        {
            if (!tomato.activeSelf)
            {
                tomato.SetActive(true);
                canEnter = false;
            }

            if (S_T_PlayerMovement.Instance.heldItem.sprite != null)
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 8)
        {
            canEnter = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 9 && !minimap.activeSelf)
        {
            minimap.SetActive(true);
        }

        if (currentLine == 10)
        {
            if (!prepCounter.activeSelf)
            {
                prepCounter.SetActive(true);
                canEnter = false;
            }

            if (S_T_PrepTable.Instance.prepTableUI.activeSelf)
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 14 && !ingredientTable.activeSelf)
        {
            ingredientTable.SetActive(true);
            S_T_PrepTable.Instance.prepTableUI.SetActive(false);
        }

        if (currentLine == 16)
        {
            canEnter = false;

            if (S_T_PlayerMovement.Instance.GrabLinkedItem(1) != null)
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 17)
        {
            if (!potato.activeSelf && !oil.activeSelf && !fish.activeSelf)
            {
                potato.SetActive(true);
                oil.SetActive(true);
                fish.SetActive(true);
                canEnter = false;
            }

            if (S_T_PlayerMovement.Instance.GrabLinkedItem(1) != null && S_T_PlayerMovement.Instance.GrabLinkedItem(2) != null && S_T_PlayerMovement.Instance.GrabLinkedItem(3) != null && S_T_PlayerMovement.Instance.GrabLinkedItem(4) != null)
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 19)
        {
            canEnter = false;

            if (S_T_PlayerMovement.Instance.heldItem.sprite == S_T_ItemManager.Instance.ingredients[5])
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 20)
        {
            if (!stove.activeSelf)
            {
                stove.SetActive(true);
                canEnter = false;
            }

            if (S_T_PlayerMovement.Instance.GrabLinkedItem(0) != null)
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine < 23 && S_T_PlayerMovement.Instance.heldItem.sprite == S_T_ItemManager.Instance.ingredients[16])
        {
            S_T_TextBoxManager.Instance.currentLine = 24;
            canEnter = true;
        }

        if (currentLine == 23)
        {
            canEnter = false;

            if (S_T_PlayerMovement.Instance.heldItem.sprite == S_T_ItemManager.Instance.ingredients[16])
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 24)
        {
            canEnter = false;

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 25)
        {
            canEnter = false;

            if (S_T_PrepTable.Instance.finishedRecipes == 1)
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 27 && !trashCan.activeSelf && !fridge.activeSelf && !cornerCounter.activeSelf)
        {
            trashCan.SetActive(true);
            fridge.SetActive(true);
            cornerCounter.SetActive(true);
        }

        if (currentLine < 29 && tomato == null)
        {
            S_T_TextBoxManager.Instance.currentLine = 30;
            canEnter = true;
        }

        if (currentLine == 29)
        {
            canEnter = false;

            if (tomato == null)
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 31 && !timer.activeSelf)
        {
            timer.SetActive(true);
        }

        if (currentLine == 33)
        {
            canEnter = false;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (currentLine == 34)
        {
            canEnter = false;

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                S_T_TextBoxManager.Instance.currentLine += 1;
                canEnter = true;
            }
        }

        if (!textBox.activeSelf)
        {
            SceneManager.LoadScene("Day1");
        }
    }
    public void SkipTutorial()
    {
        SceneManager.LoadScene("Day1");
    }
}
