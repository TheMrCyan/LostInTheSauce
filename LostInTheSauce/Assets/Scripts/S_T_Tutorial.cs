using UnityEngine;

public class S_T_Tutorial : MonoBehaviour
{
    public GameObject staminaBar;
    public GameObject minimap;
    private bool createdRecipe;

    void Start()
    {
        staminaBar.SetActive(false);
        minimap.SetActive(false);
    }

    void Update()
    {
        if (createdRecipe == false)
        {
            S_T_PrepTable.Instance.recipeBook.Clear();
            S_T_PrepTable.Instance.recipeBook.Add(new Recipe("Chocolate Cake", new int[] { (int)Food.Dough, (int)Food.Sugar, (int)Food.Chocolate }));
            createdRecipe = true;
        }
    }
}
