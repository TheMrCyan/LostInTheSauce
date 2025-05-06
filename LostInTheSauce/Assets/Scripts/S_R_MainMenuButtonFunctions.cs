using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_R_MainMenuButtonFunctions : MonoBehaviour
{
   public void PlayButton()
    {
        SceneManager.LoadScene("Day1");
    }
    public void InstructionsButton()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void OptionButton()
    {
        SceneManager.LoadScene("Options");
    }
    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }
    public void QuitButton()
    {
        Application.Quit();
    }

}
