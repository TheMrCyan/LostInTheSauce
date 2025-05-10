using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_T_PrepTable : MonoBehaviour
{
    public static S_T_PrepTable Instance { get; private set; }

    private bool touchingPlayer;
    private List<Recipe> recipeBook;
    private int recipeToShow;
    public GameObject prepTableUI;
    public TextMeshProUGUI recipeTitle;
    public List<Image> recipeBookIngredients;
    public List<int> ingredientsToRemove;


    [SerializeField, Tooltip("Amount of recipe's the player needs to make")] private int m_NumberOfRecipes;
    [SerializeField, Tooltip("Amount of recipe's the player needs to make")] private int m_FinishedRecipes = 0;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        recipeBook = new List<Recipe>();
        recipeBook.Add(new Recipe("Pizza", new int[] { (int)Food.Dough, (int)Food.Tomato, (int)Food.Meat, (int)Food.Cheese }));
        recipeBook.Add(new Recipe("Burger", new int[] { (int)Food.Bread, (int)Food.CookedMeat, (int)Food.Tomato, (int)Food.Lettuce, (int)Food.Cheese }));
        recipeBook.Add(new Recipe("Sushi", new int[] { (int)Food.Fish, (int)Food.Rice }));
        recipeBook.Add(new Recipe("Cookies", new int[] { (int)Food.Dough, (int)Food.Sugar, (int)Food.Chocolate }));
        recipeBook.Add(new Recipe("Kebab", new int[] { (int)Food.Bread, (int)Food.Garlic, (int)Food.CookedMeat, (int)Food.Lettuce }));
        recipeBook.Add(new Recipe("Fries", new int[] { (int)Food.Potato, (int)Food.Oil }));

        // Prepare first recipe
        UpdateRecipe(0);

        prepTableUI.SetActive(false);
    }

    private void Update()
    {
        if (touchingPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            // First update recipe in case fridge contents were updated
            UpdateRecipe(recipeToShow);

            prepTableUI.SetActive(!prepTableUI.activeSelf);
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
            if (prepTableUI != null)
            {
                prepTableUI.SetActive(false);
            }
        }
    }

    private void UpdateRecipe(int recipeId)
    {
        // Clear all images first
        foreach (Image image in recipeBookIngredients)
        {
            image.gameObject.SetActive(false);
        }
        ingredientsToRemove.Clear();

        recipeTitle.text = recipeBook[recipeId].title;
        for (int i = 0; i < recipeBook[recipeId].ingredients.Length; i++)
        {
            recipeBookIngredients[i].gameObject.SetActive(true);
            recipeBookIngredients[i].transform.GetChild(0).gameObject.SetActive(false);
            // Check if you have the ingredient in the fridge
            for (int j = 0; j < S_T_Fridge.Instance.fridgeContents.Length; j++)
            {
                if (recipeBook[recipeToShow].ingredients[i] == S_T_Fridge.Instance.fridgeContents[j])
                {
                    // Enable "OK" overlay
                    recipeBookIngredients[i].transform.GetChild(0).gameObject.SetActive(true);
                    ingredientsToRemove.Add(j);
                    break;
                }
            }
            recipeBookIngredients[i].sprite = S_T_ItemManager.Instance.ingredients[recipeBook[recipeId].ingredients[i]];
        }
    }

    public void NextRecipe(int direction)
    {
        recipeToShow = recipeToShow += direction;
        recipeToShow = Mathf.Clamp(recipeToShow, 0, recipeBook.Count - 1);
        UpdateRecipe(recipeToShow);
    }

    public void MakeRecipe()
    {
        bool foundIngredient;
        bool failedRecipe = false;

        // Go through fridge ingredients
        for (int i = 0; i < recipeBook[recipeToShow].ingredients.Length; i++)
        {
            foundIngredient = false;
            for (int j = 0; j < S_T_Fridge.Instance.fridgeContents.Length; j++)
            {
                if (recipeBook[recipeToShow].ingredients[i] == S_T_Fridge.Instance.fridgeContents[j])
                {
                    Debug.Log("Found " + recipeBook[recipeToShow].ingredients[i] + " in the fridge!");
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
            Debug.Log("Made " + recipeBook[recipeToShow].title + "!");
            // Remove ingredients from fridge
            foreach (int id in ingredientsToRemove)
            {
                S_T_Fridge.Instance.fridgeContents[id] = -1;
                S_T_Fridge.Instance.fridgeVisuals[id].sprite = null;
            }
            // Close menu
            prepTableUI.SetActive(false);

            //code under this will send you to "EndOfTheDay" once all recipe's are made
            CompletedRecipe();
            CompletionChecker();
        }
        else
        {
            Debug.Log("Failed to make " + recipeBook[recipeToShow].title + "!");
        }

       
        
    }
    public void CompletionChecker()
    {
        if (m_FinishedRecipes == m_NumberOfRecipes)
        {
            SceneManager.LoadScene("EndOfTheDay");
        }
    }
    public void CompletedRecipe()
    {
        ++m_FinishedRecipes;
    }
}

public class Recipe
{
    public string title;
    public int[] ingredients;

    public Recipe(string title, int[] ingredients)
    {
        this.title = title;
        this.ingredients = ingredients;
    }
}