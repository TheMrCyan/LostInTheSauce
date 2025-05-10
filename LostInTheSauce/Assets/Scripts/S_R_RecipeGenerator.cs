using UnityEngine;
using UnityEngine.SceneManagement;

public class S_R_RecipeNumber : MonoBehaviour
{
    [SerializeField, Tooltip("Amount of recipe's the player needs to make")] private int m_NumberOfRecipes;
    [SerializeField, Tooltip("Amount of recipe's the player needs to make")] private int m_FinishedRecipes = 0;

    public void SetDay1()
    {
        m_NumberOfRecipes = 1;
    }
    public void SetDay2()
    {
        m_NumberOfRecipes = 2;
    }
    public void SetDay3()
    {
        m_NumberOfRecipes = 3;
    }
    public void SetDay4()
    {
        m_NumberOfRecipes = 5;
    }
    public void SetDay5()
    {
        m_NumberOfRecipes = 6;
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
