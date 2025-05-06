using UnityEngine;
using UnityEngine.SceneManagement;

public class S_R_NextDay : MonoBehaviour
{

    [SerializeField][Tooltip("This number is the day you're currently on")] private int m_DayCount = 1;


    public void NextLevel()
    {
        switch (m_DayCount)
        {
            case 1:
                ++m_DayCount;

                break;
            case 2:
                ++m_DayCount;


                break;
            case 3:
                ++m_DayCount;


                break;
            case 4:
                ++m_DayCount;


                break;
            case 5:
                ++m_DayCount;

                break;
        }
    }


    public void GiveUp()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
