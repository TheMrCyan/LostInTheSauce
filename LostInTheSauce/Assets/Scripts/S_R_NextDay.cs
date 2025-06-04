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

    private void Awake()
    {
        m_firstButtons.SetActive(true);
        m_secondButtons.SetActive(false);
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
        S_R_SkillManager.Skill1Unlocked = true;
        m_firstButtons.SetActive(true);
        m_secondButtons.SetActive(false);
        string sceneName = "Day" + m_DayCount;
        SceneManager.LoadScene(sceneName);
        m_DayCount++;

        Destroy(skill1);

    }
    public void Skill2Unlock()
    {
        S_R_SkillManager.Skill2Unlocked = true;
        m_firstButtons.SetActive(true);
        m_secondButtons.SetActive(false);
        string sceneName = "Day" + m_DayCount;
        SceneManager.LoadScene(sceneName);
        m_DayCount++;

        Destroy(skill2);
    }
    public void Skill3Unlock()
    {
        S_R_SkillManager.Skill3Unlocked = true;
        m_firstButtons.SetActive(true);
        m_secondButtons.SetActive(false);
        string sceneName = "Day" + m_DayCount;
        SceneManager.LoadScene(sceneName);
        m_DayCount++;

        Destroy(skill3);
    }
    public void Skill4Unlock()
    {
        S_R_SkillManager.Skill4Unlocked = true;
        m_firstButtons.SetActive(true);
        m_secondButtons.SetActive(false);
        string sceneName = "Day" + m_DayCount;
        SceneManager.LoadScene(sceneName);
        m_DayCount++;

        Destroy(skill4);
    }
}