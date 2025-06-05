using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_T_PrepTable : MonoBehaviour
{
    public static S_T_PrepTable Instance { get; private set; }

    public bool isTutorial = false;
    private bool touchingPlayer;
    public int finishedRecipes;
    public List<Recipe> recipeBook;
    private int recipeToShow;
    public int recipesToMake;
    public int availableRecipes;
    public GameObject prepTableUI;
    public TextMeshProUGUI recipeTitle;
    public List<Image> recipeBookIngredients;
    public List<int> ingredientsToRemove;
    [SerializeField] private float extraTimePerIngredient = 30f;

    private void Awake()
    {
        Instance = this;
    }

    private int GetCurrentDay()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.StartsWith("Day"))
        {
            string dayNumber = sceneName.Replace("Day", "");
            if (int.TryParse(dayNumber, out int result))
            {
                return result;
            }
        }
        return 1;
    }

    private void Start()
    {
        recipeBook = new List<Recipe>();
        recipeBook.Add(new Recipe("Burger", new int[] { (int)Food.Bread, (int)Food.CookedMeat, (int)Food.Tomato, (int)Food.Lettuce, (int)Food.Cheese }));
        recipeBook.Add(new Recipe("Pizza", new int[] { (int)Food.Dough, (int)Food.Tomato, (int)Food.Meat, (int)Food.Cheese }));
        recipeBook.Add(new Recipe("Chocolate Cake", new int[] { (int)Food.Dough, (int)Food.Sugar, (int)Food.Chocolate }));
        recipeBook.Add(new Recipe("Salad", new int[] { (int)Food.Tomato, (int)Food.Lettuce, (int)Food.Cucumber }));
        recipeBook.Add(new Recipe("Sushi", new int[] { (int)Food.Fish, (int)Food.Rice }));
        recipeBook.Add(new Recipe("Fish & Chips", new int[] { (int)Food.Potato, (int)Food.Oil, (int)Food.CookedFish }));
        recipeBook.Add(new Recipe("Dumplings", new int[] { (int)Food.Dough, (int)Food.Meat, (int)Food.Cheese }));
        recipeBook.Add(new Recipe("Bacon & Eggs", new int[] { (int)Food.CookedMeat, (int)Food.CookedEgg }));
        recipeBook.Add(new Recipe("Spaghetti", new int[] { (int)Food.Dough, (int)Food.Tomato, (int)Food.Garlic }));
        recipeBook.Add(new Recipe("Sundae", new int[] { (int)Food.Milk, (int)Food.Sugar }));
        recipeBook.Add(new Recipe("Sandwich", new int[] { (int)Food.Bread, (int)Food.Cucumber, (int)Food.Tomato, (int)Food.Cheese, (int)Food.Meat }));
        recipeBook.Add(new Recipe("Fries", new int[] { (int)Food.Potato, (int)Food.Oil }));
        recipeBook.Add(new Recipe("Garlic Bread", new int[] { (int)Food.Bread, (int)Food.Garlic, (int)Food.Cheese }));
        recipeBook.Add(new Recipe("Kebab", new int[] { (int)Food.Bread, (int)Food.Garlic, (int)Food.CookedMeat, (int)Food.Lettuce }));
        recipeBook.Add(new Recipe("Chocolate Milk", new int[] { (int)Food.Cocoa, (int)Food.Milk }));

        SelectDailyRecipes();

        // Prepare first recipe
        UpdateRecipe(0);

        prepTableUI.SetActive(false);
    }

    private void Update()
    {
        if (!S_T_PauseMenu.isPaused)
        {
            if (touchingPlayer && Input.GetKeyDown(KeyCode.Space))
            {
                // First update recipe in case fridge contents were updated
                UpdateRecipe(recipeToShow);

                prepTableUI.SetActive(!prepTableUI.activeSelf);
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
                if (!isTutorial)
                {
                    S_T_PlayerMovement.Instance.GrabLinkedItem(id + 1).RandomizeLocation();
                }
                else
                {
                    Destroy(S_T_PlayerMovement.Instance.GrabLinkedItem(id + 1));
                }
                S_T_Fridge.Instance.fridgeContents[id] = -1;
                S_T_Fridge.Instance.fridgeVisuals[id].sprite = null;
                S_R_ShiftTimer.Instance.shiftTime += extraTimePerIngredient;
            }
            // Close menu
            prepTableUI.SetActive(false);

            // Finish day if enough recipes have been made
            ++finishedRecipes;
            if (!isTutorial)
            {
                if (finishedRecipes >= recipesToMake)
                {
                    var curDay = GetCurrentDay();
                    if (curDay < 5)
                    {
                        SceneManager.LoadScene("EndOfTheDay");
                    }
                    if (curDay == 5)
                    {
                        SceneManager.LoadScene("VictoryScreen");
                    }
                }
            }
        }
        else
        {
            Debug.Log("Failed to make " + recipeBook[recipeToShow].title + "!");
        }
    }

    private void SelectDailyRecipes()
    {
        // Make sure available recipes is never bigger than the original book
        availableRecipes = Mathf.Clamp(availableRecipes, 0, recipeBook.Count);

        // Create a copy of the original recipe list
        List<Recipe> tempRecipes = new List<Recipe>(recipeBook);

        // Fisher-Yates shuffle algorithm
        for (int i = tempRecipes.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Recipe temp = tempRecipes[i];
            tempRecipes[i] = tempRecipes[randomIndex];
            tempRecipes[randomIndex] = temp;
        }

        // Replace the original recipe book with daily selection
        recipeBook.Clear();
        recipeBook.AddRange(tempRecipes.GetRange(0, availableRecipes));
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