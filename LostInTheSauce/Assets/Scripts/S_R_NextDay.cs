using UnityEngine;
using UnityEngine.SceneManagement;

public class S_R_NextDay : MonoBehaviour
{
    [SerializeField, Tooltip("Total number of days/scenes")]
    private int m_TotalDays = 5;

    public static int m_DayCount = 2; // Static, persists between scenes

    [SerializeField] private GameObject m_firstButtons;
    [SerializeField] private GameObject m_secondButtons;
    [SerializeField] private GameObject skill1, skill2, skill3, skill4;
    static private bool m_skill1unlocked, m_skill2unlocked, m_skill3unlocked, m_skill4unlocked;

    private void Awake()
    {
        m_firstButtons.SetActive(true);
        m_secondButtons.SetActive(false);
        if (m_skill1unlocked )
        {
            Destroy(skill1);
        }
        if (m_skill2unlocked )
        {
            Destroy(skill2);
        }
        if (m_skill3unlocked )
        {
            Destroy(skill3);
        }
        if (m_skill4unlocked )
        {
            Destroy(skill4);
        }
    }


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
        SceneManager.LoadScene("YouDestroyedTheKitchen");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("R_HomeScreen");
        m_DayCount = 2;

    }
    public void SkillsScreen()
    {
        
        if (m_DayCount <= m_TotalDays)
        {
            m_firstButtons.SetActive(false);
            m_secondButtons.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
    public void Skill1Unlock()
    {
        S_T_SkillUnlock.Skill1Unlocked = true;
        m_firstButtons.SetActive(true);
        m_secondButtons.SetActive(false);
        string sceneName = "Day" + m_DayCount;
        SceneManager.LoadScene(sceneName);
        m_DayCount++;
        m_skill1unlocked = true;
        Destroy(skill1);

    }
    public void Skill2Unlock()
    {
        S_T_SkillUnlock.Skill2Unlocked = true;
        m_firstButtons.SetActive(true);
        m_secondButtons.SetActive(false);
        string sceneName = "Day" + m_DayCount;
        SceneManager.LoadScene(sceneName);
        m_DayCount++;
        m_skill2unlocked = true;
        Destroy(skill2);
    }
    public void Skill3Unlock()
    {
        S_T_SkillUnlock.Skill3Unlocked = true;
        m_firstButtons.SetActive(true);
        m_secondButtons.SetActive(false);
        string sceneName = "Day" + m_DayCount;
        SceneManager.LoadScene(sceneName);
        m_DayCount++;
        m_skill3unlocked = true;
        Destroy(skill3);
    }
    public void Skill4Unlock()
    {
        S_T_SkillUnlock.Skill4Unlocked = true;
        m_firstButtons.SetActive(true);
        m_secondButtons.SetActive(false);
        string sceneName = "Day" + m_DayCount;
        SceneManager.LoadScene(sceneName);
        m_DayCount++;
        m_skill4unlocked = true;
        Destroy(skill4);
    }
}