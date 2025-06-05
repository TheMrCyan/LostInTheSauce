using System.Data;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class S_T_SkillTimers : MonoBehaviour
{
    private float time1;
    private float time2;
    private float time3;
    private float time4;

    public GameObject timer1;
    public TMP_Text timerValue1;
    public UnityEngine.UI.Image timerImage1;

    public GameObject timer2;
    public TMP_Text timerValue2;
    public UnityEngine.UI.Image timerImage2;

    public GameObject timer3;
    public TMP_Text timerValue3;
    public UnityEngine.UI.Image timerImage3;

    public GameObject timer4;
    public TMP_Text timerValue4;
    public UnityEngine.UI.Image timerImage4;

    private float max1;
    private float max2;
    private float max3;
    private float max4;

    private float countdown1;
    private float countdown2;
    private float countdown3;
    private float countdown4;

    private void Start()
    {
        max1 = S_R_SkillManager.Instance.skill1Cooldown;
        max2 = S_R_SkillManager.Instance.skill2Cooldown;
        max3 = S_R_SkillManager.Instance.skill3Cooldown;
        max4 = S_R_SkillManager.Instance.skill4Cooldown;
        timer1.SetActive(false);
        timer2.SetActive(false);
        timer3.SetActive(false);
        timer4.SetActive(false);
    }
    private void Update()
    {
        if (S_R_SkillManager.Instance.Skill1Unlocked)
        {
            timer1.SetActive(true);
        }
        if (S_R_SkillManager.Instance.Skill2Unlocked)
        {
            timer2.SetActive(true);
        }
        if (S_R_SkillManager.Instance.Skill3Unlocked)
        {
            timer3.SetActive(true);
        }
        if (S_R_SkillManager.Instance.Skill4Unlocked)
        {
            timer4.SetActive(true);
        }
        time1 = Time.time - S_R_SkillManager.Instance.skill1LastUsedTime;
        time1 = Mathf.Clamp(time1, 0, max1);

        countdown1 = max1 - time1;
        timerValue1.text = "" + (int)countdown1;
        timerImage1.fillAmount = time1 / max1;


        time2 = Time.time - S_R_SkillManager.Instance.skill2LastUsedTime;
        time2 = Mathf.Clamp(time2, 0, max2);

        countdown2 = max2 - time2;
        timerValue2.text = "" + (int)countdown2;
        timerImage2.fillAmount = time2 / max2;


        time3 = Time.time - S_R_SkillManager.Instance.skill3LastUsedTime;
        time3 = Mathf.Clamp(time3, 0, max3);

        countdown3 = max3 - time3;
        timerValue3.text = "" + (int)countdown3;
        timerImage3.fillAmount = time3 / max3;

        time4 = Time.time - S_R_SkillManager.Instance.skill4LastUsedTime;
        time4 = Mathf.Clamp(time4, 0, max4);

        countdown4 = max4 - time4;
        timerValue4.text = "" + (int)countdown4;
        timerImage4.fillAmount = time4 / max4;
    }
}