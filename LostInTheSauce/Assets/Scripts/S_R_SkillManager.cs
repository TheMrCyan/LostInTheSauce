using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class S_R_SkillManager : MonoBehaviour
{
    [SerializeField, Tooltip("This value will determine whether the skill is unlocked or not")] static public bool Skill1Unlocked = false;
    [SerializeField, Tooltip("This value will determine whether the skill is unlocked or not")] static public bool Skill2Unlocked = false;
    [SerializeField, Tooltip("This value will determine whether the skill is unlocked or not")] static public bool Skill3Unlocked = false;
    [SerializeField, Tooltip("This value will determine whether the skill is unlocked or not")] static public bool Skill4Unlocked = false;
    [SerializeField, Tooltip("Input player here")] private GameObject m_Player;

  
    //skill 3
    private Vector3 OldPos;
    private Vector3 NewPos;
    Collider2D hit;
    [SerializeField, Tooltip("Cooldown of skill 3 in seconds")] private float skill3Cooldown = 3f;
    private float skill3LastUsedTime = -Mathf.Infinity;

 

    private void Update()
    {
       // ===================================== SKILL 3 ======================================
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryUseSkill3(1, 5f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TryUseSkill3(2, 5f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TryUseSkill3(3, 5f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TryUseSkill3(4, 5f);
        }

    }



    [Tooltip("Spawn one random item")]
    private void Skill1() //speedboost
    {
        //put skill code here
    }

    [Tooltip("Spawn one random item")]
    private void Skill2() //spawn one random item
    {

    }

    [Tooltip("Spawn one random item")] //teleport forward 5 units
    private void Skill3(int i, float range)
    {

        OldPos = m_Player.transform.position;

        switch (i)
        {
            case 1:
                OldPos = m_Player.transform.position;
                NewPos = OldPos + m_Player.transform.up * range;
                hit = Physics2D.OverlapPoint(NewPos);
                if (hit != null && hit.CompareTag("Floor"))
                {
                    m_Player.transform.position = NewPos;
                    m_Player.transform.position = NewPos;
                    range = 5;
                    skill3LastUsedTime = Time.time;
                }
                else
                {
                    Skill3(i, range + 0.1f);
                }

                break;
            case 2:
                OldPos = m_Player.transform.position;
                NewPos = OldPos + m_Player.transform.up * -range;
                hit = Physics2D.OverlapPoint(NewPos);
                if (hit != null && hit.CompareTag("Floor"))
                {
                    m_Player.transform.position = NewPos;
                    m_Player.transform.position = NewPos;
                    range = 5;
                    skill3LastUsedTime = Time.time;
                }
                else
                {
                    Skill3(i, range + 0.1f);
                }
                break;
            case 3:
                OldPos = m_Player.transform.position;
                NewPos = OldPos + Vector3.left * range;
                hit = Physics2D.OverlapPoint(NewPos);
                if (hit != null && hit.CompareTag("Floor"))
                {
                    m_Player.transform.position = NewPos;
                    m_Player.transform.position = NewPos;
                    range = 5;
                    skill3LastUsedTime = Time.time;
                }
                else
                {
                    Skill3(i, range + 0.1f);
                }
                break;
                    case 4:
                OldPos = m_Player.transform.position;
                NewPos = OldPos + Vector3.right * range;
                hit = Physics2D.OverlapPoint(NewPos);
                if (hit != null && hit.CompareTag("Floor"))
                {
                    m_Player.transform.position = NewPos;
                    m_Player.transform.position = NewPos;
                    range = 5;
                    skill3LastUsedTime = Time.time;
                }
                else if (range < 6)
                {
                   
                    Skill3(i, range + 0.1f);
                }
                break;

        }
    }

    [Tooltip("Spawn one random item")]
    private void Skill4() //instantly process a held food item
    {
        //put skill code here
    }






    private void TryUseSkill3(int i, float range)
    {
        if (Time.time - skill3LastUsedTime >= skill3Cooldown)
        {
            Skill3(i, range);
           
        }
        else
        {
            Debug.Log("Skill 3 is on cooldown!");
        }
    }

}
