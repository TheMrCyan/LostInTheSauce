using System.Data;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_T_StoveTimer : MonoBehaviour
{
    public bool inTutorial = false;
    private bool useGraceTime;
    private float time;
    public TMP_Text timerValue;
    public UnityEngine.UI.Image timerImage;
    public GameObject timer;
    private float max;

    private void Start()
    {
        max = S_T_Stove.Instance.perfectDoneTime;
        timerImage.color = Color.yellow;
        timer.SetActive(false);
    }
    private void Update()
    {
        if (S_T_Stove.Instance.heldItem.sprite != null)
        {
            timer.SetActive(true);
            if (useGraceTime)
            {
                if (!inTutorial)
                {
                    time = max - S_T_Stove.Instance.graceTimer;
                }
            }
            else
            {
                time = max - S_T_Stove.Instance.cookingTime;
            }
            timerValue.text = "" + (int)time;
            timerImage.fillAmount = time / max;

            if (S_T_Stove.Instance.cooked && useGraceTime == false)
            {
                useGraceTime = true;
                timerImage.color = Color.red;
                max = S_T_Stove.Instance.graceTime;
            }
            else if (!S_T_Stove.Instance.cooked && useGraceTime == true)
            {
                useGraceTime = false;
                timerImage.color = Color.yellow;
                max = S_T_Stove.Instance.perfectDoneTime;
            }
        }
        else if (timer.activeSelf)
        {
            timer.SetActive(false);
        }
    }
}