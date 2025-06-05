using UnityEngine;
using TMPro; // Import this for TextMeshPro
using UnityEngine.SceneManagement;


public class S_R_ShiftTimer : MonoBehaviour
{
    public bool inTutorial = false;

    public static S_R_ShiftTimer Instance { get; private set; }

    [SerializeField][Tooltip("The Duration of the game")] public float shiftTime = 60.0f;
    [SerializeField] private TMP_Text timerText;

    private bool timerEndedFlag = false;
    static public bool hasStarted = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!inTutorial)
        {
            if (hasStarted)
            {
                if (timerEndedFlag) return;

                shiftTime -= Time.deltaTime;
                shiftTime = Mathf.Max(shiftTime, 0); // Prevents negative time

                // Format time as MM:SS
                int minutes = Mathf.FloorToInt(shiftTime / 60f);
                int seconds = Mathf.FloorToInt(shiftTime % 60f);
                string timer = string.Format("{0:00}:{1:00}", minutes, seconds);
                timerText.text = timer;

                if (shiftTime <= 0.0f)
                {
                    timerEndedFlag = true;
                    TimerEnded();
                }
            }
        }
    }
    public void UnderstoodButton()
    {
        hasStarted = true;
    }
    private void TimerEnded()
    {
        Debug.Log("Game Over!");
        S_T_SkillUnlock.Skill1Unlocked = false;
        S_T_SkillUnlock.Skill2Unlocked = false;
        S_T_SkillUnlock.Skill3Unlocked = false;
        S_T_SkillUnlock.Skill4Unlocked = false;
        SceneManager.LoadScene("GameOverScene"); //loads the GameOver scene
    }
}