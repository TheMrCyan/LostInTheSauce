using UnityEngine;
using TMPro; // Import this for TextMeshPro
using UnityEngine.SceneManagement;


public class SimpleTimer : MonoBehaviour
{
    [SerializeField] [Tooltip("The time you've got left on your shift")]private float ShiftTime = 60.0f;
    [SerializeField]private TMP_Text timerText; // Drag your TimerText object here in Inspector

    private bool timerEndedFlag = false;

    private void Update()
    {
        if (timerEndedFlag) return;

        ShiftTime -= Time.deltaTime;
        ShiftTime = Mathf.Max(ShiftTime, 0); // Prevent negative time

        // Format time as MM:SS
        int minutes = Mathf.FloorToInt(ShiftTime / 60f);
        int seconds = Mathf.FloorToInt(ShiftTime % 60f);
        timerText.text = string.Format("Time Left for your shift: {0:00}:{1:00} ", minutes, seconds);

        if (ShiftTime <= 0.0f)
        {
            timerEndedFlag = true;
            TimerEnded();
        }
    }

    void TimerEnded()
    {
        // Show Game Over screen here
        // For now, just log
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOverScene");
    }
}