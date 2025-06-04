using System;
using System.Collections;
using System.Collections.Generic;
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

    //skill 1
    public S_T_PlayerMovement m_MovementSpeed;
    private float speedBoostDuration = 3f;
    [SerializeField, Tooltip("Cooldown of skill 1 in seconds")] private float skill1Cooldown = 15f;
    private float skill1LastUsedTime = -Mathf.Infinity;

    //skill 2
    public S_T_ItemGen ingredient;
    public int ingredientsToSpawn = 4;
    [System.NonSerialized] public int newID;
    public Sprite[] ingredients;
    [SerializeField] private Transform itemManager;
    public SpriteRenderer[] visuals;
    private int lastRawIngredient = 14;
    [SerializeField, Tooltip("Cooldown of skill 2 in seconds")] private float skill2Cooldown = 120f;
    private float skill2LastUsedTime = -Mathf.Infinity;

    //skill 3
    private Vector3 OldPos;
    private Vector3 NewPos;
    Collider2D hit;
    [SerializeField, Tooltip("Cooldown of skill 3 in seconds")] private float skill3Cooldown = 25f;
    private float skill3LastUsedTime = -Mathf.Infinity;

    //skill 4
    private int ingredientId;
    private float cookingTime;
    private float graceTimer;
    public float perfectDoneTime;
    public float graceTime;
    public SpriteRenderer heldItem;
    static public bool SkillUsed;
    [SerializeField, Tooltip("Cooldown of skill 1 in seconds")] private float skill4Cooldown = 300f;
    private float skill4LastUsedTime = -Mathf.Infinity;



    private void Update()
    {
        if (!S_T_PauseMenu.isPaused)
        {
            // ===================================== SKILL 1 ======================================
            if (Input.GetKeyDown(KeyCode.V))
            {
                TryUseSkill1();
            }
            // ===================================== SKILL 2 ======================================
            if (Input.GetKeyDown(KeyCode.Y))
            {
                TryUseSkill2();
            }
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
            // ===================================== SKILL 4 ======================================
            if (Input.GetKeyDown(KeyCode.B))
            {
                TryUseSkill4();
            }
            // ===================================== DEBUG BUTTON ======================================
            if(Input.GetKeyDown(KeyCode.Keypad7))
            {
                Skill1Unlocked = true;
                Skill2Unlocked = true;
                Skill3Unlocked = true;
                Skill4Unlocked = true;
            }
        }
    }

    [Tooltip("Spawn one random item")]
    private void Skill1() //speedboost
    {
        m_MovementSpeed.SetSpeed(12f);
        StartCoroutine(ResetSpeedAfterDelay());
    }

    private IEnumerator ResetSpeedAfterDelay()
    {
        yield return new WaitForSeconds(speedBoostDuration);
        m_MovementSpeed.ResetSpeed();
    }

    [Tooltip("Spawn one random item")]
    private void Skill2() //spawn one random item
    {

        for (int i = 0; i < 1; i++)
        {
            S_T_ItemGen ing = Instantiate(ingredient, m_Player.transform.position, Quaternion.identity);
            ing.id = i;

            ing.visuals[0].sprite = S_T_ItemManager.Instance.ingredients[UnityEngine.Random.Range(0, lastRawIngredient + 1)];
            ing.visuals[1].sprite = ing.visuals[0].sprite; // Minimap
        }
        newID = ingredientsToSpawn;


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
        // Increase time while cooking

        var ingredient = S_T_PlayerMovement.Instance.GrabLinkedItem(7);

        for (int i = 0; i < S_T_ItemManager.Instance.ingredients.Length; i++)
        {
            if (S_T_PlayerMovement.Instance.heldItem.sprite == S_T_ItemManager.Instance.ingredients[i])
            {
                ingredientId = i;
                break;
            }
        }

        switch (ingredientId)
        {
            case (int)Food.Dough:
                heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.Bread];
                break;
            case (int)Food.Fish:
                heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.CookedFish];
                break;
            case (int)Food.Meat:
                heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.CookedMeat];
                break;
            case (int)Food.Egg:
                heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.CookedEgg];
                break;
            case (int)Food.Cocoa:
                heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.Chocolate];
                break;
            default:
                heldItem.sprite = null;
                break;
        }
        // Update the physical item
        ingredient.visuals[0].sprite = heldItem.sprite;
        ingredient.visuals[1].sprite = heldItem.sprite;
    }



    private void TryUseSkill3(int i, float range)
    {
        if (Skill3Unlocked)
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
    private void TryUseSkill2()
    {
        if (Skill2Unlocked)
        {
            if (Time.time - skill2LastUsedTime >= skill2Cooldown)
            {
                Skill2();
                skill2LastUsedTime = Time.time;

            }
            else
            {
                Debug.Log("Skill 2 is on cooldown!");
            }
        }
    }

    private void TryUseSkill1()
    {
        if (Skill1Unlocked)
        {
            if (Time.time - skill1LastUsedTime >= skill1Cooldown)
            {
                Skill1();
                skill1LastUsedTime = Time.time;
            }
            else
            {
                Debug.Log("Skill 1 is on cooldown!");
            }
        }
    }
    private void TryUseSkill4()
    {
        if (Skill4Unlocked)
        {


            if (Time.time - skill4LastUsedTime >= skill4Cooldown)
            {
                Skill1();
                skill4LastUsedTime = Time.time;
            }
            else
            {
                Debug.Log("Skill 4 is on cooldown!");
            }
        }

    }
}