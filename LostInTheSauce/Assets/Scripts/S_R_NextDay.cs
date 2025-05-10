using UnityEngine;
using UnityEngine.SceneManagement;

public class S_R_NextDay : MonoBehaviour
{
    [SerializeField, Tooltip("Total number of days/scenes")]
    private int m_TotalDays = 5;

    public static int m_DayCount = 2; // Static, persists between scenes

    public void NextLevel()
    {
        if (m_DayCount <= m_TotalDays)
        {
            string sceneName = "Day" + m_DayCount;
            SceneManager.LoadScene(sceneName);
            m_DayCount++;
        }
        else
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }

    public void GiveUp()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}