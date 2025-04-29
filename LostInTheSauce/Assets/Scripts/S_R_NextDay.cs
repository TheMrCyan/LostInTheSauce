using UnityEngine;
using UnityEngine.SceneManagement;

public class S_R_NextDay : MonoBehaviour
{



private void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void GiveUp()
    {
        SceneManager.LoadScene("EndOfTheDay");
    }
}
