using UnityEngine;
using UnityEngine.SceneManagement;

public class S_R_GameOver : MonoBehaviour
{

   public void MainMenuButton()
    {
        SceneManager.LoadScene("R_HomeScreen");
        S_R_NextDay.m_DayCount = 2;

    }
}