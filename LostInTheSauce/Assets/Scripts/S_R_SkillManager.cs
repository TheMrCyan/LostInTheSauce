using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class S_R_SkillManager : MonoBehaviour
{
    [SerializeField, Tooltip("This value will determine whether the skill is unlocked or not")] static public bool Skill1Unlocked = false;
    [SerializeField, Tooltip("This value will determine whether the skill is unlocked or not")] static public bool Skill2Unlocked = false;
    [SerializeField, Tooltip("This value will determine whether the skill is unlocked or not")] static public bool Skill3Unlocked = false;
    [SerializeField, Tooltip("This value will determine whether the skill is unlocked or not")] static public bool Skill4Unlocked = false;
    [SerializeField, Tooltip("This is the value determining how far forward the item spawns in front of the player")] private float distance = 2f;
    [SerializeField, Tooltip("Input player here")] private GameObject m_Player;

    public bool held;
    private bool touchingPlayer;
    private int lastRawIngredient = 14;
    public int id;
    public SpriteRenderer[] visuals;
    public S_T_ItemGen ingredient;

    private Vector3 OldPos;
    private Vector3 NewPos;

    private void Awake()
    {
        visuals = GetComponentsInChildren<SpriteRenderer>(true);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Skill3(1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Skill3(2);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Skill3(3);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Skill3(4);
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
    private void Skill3(int i)
    {

        OldPos = m_Player.transform.position;

        switch (i)
        {
       
        
            case 1:
                OldPos = m_Player.transform.position;
                NewPos = OldPos + m_Player.transform.up * 5f;
                Collider2D hit = Physics2D.OverlapPoint(NewPos);
                if (hit != null && hit.CompareTag("Wall"))
                {
                    // There is something at the destination
                }
                m_Player.transform.position = NewPos;
                m_Player.transform.position = NewPos;
                break;
            case 2:
                NewPos = OldPos + m_Player.transform.up * -5f;
                m_Player.transform.position = NewPos;
                m_Player.transform.position = NewPos;
                break;
            case 3:
                NewPos = OldPos + Vector3.left.normalized * 5f;
                m_Player.transform.position = NewPos;
                m_Player.transform.position = NewPos;
                break; 
            case 4:
                NewPos = OldPos + Vector3.right.normalized * 5f;
                m_Player.transform.position = NewPos;
                m_Player.transform.position = NewPos;
                break;
        }
    }

    [Tooltip("Spawn one random item")]
    private void Skill4() //instantly process a held food item
    {
        //put skill code here
    }
}
