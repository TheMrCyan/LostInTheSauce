using UnityEngine;
using TMPro; // Import this for TextMeshPro
using UnityEngine.SceneManagement;


public class SimpleTimer : MonoBehaviour
{
    [SerializeField] [Tooltip("The Duration of the game")]private float ShiftTime = 60.0f;
    [SerializeField]private TMP_Text timerText; 

    private bool timerEndedFlag = false;

    private void Update()
    {
        if (timerEndedFlag) return;

        ShiftTime -= Time.deltaTime;
        ShiftTime = Mathf.Max(ShiftTime, 0); // Prevents negative time

        // Format time as MM:SS
        int minutes = Mathf.FloorToInt(ShiftTime / 60f);
        int seconds = Mathf.FloorToInt(ShiftTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (ShiftTime <= 0.0f)
        {
            timerEndedFlag = true;
            TimerEnded();
        }

    }

    void TimerEnded()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("EndOfTheDay"); //loads the GameOver scene
    }
}