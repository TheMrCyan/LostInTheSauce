using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



public class S_R_SkillManager : MonoBehaviour
{
    public static S_R_SkillManager Instance { get; private set; }

    public bool Skill1Unlocked = false;
    public bool Skill2Unlocked = false;
    public bool Skill3Unlocked = false;
    public bool Skill4Unlocked = false;
    [SerializeField, Tooltip("Input player here")] private GameObject m_Player;

    //skill 1
    public S_T_PlayerMovement m_MovementSpeed;
    private float speedBoostDuration = 3f;
    public float skill1Cooldown = 15f;
    public float skill1LastUsedTime = -Mathf.Infinity;

    //skill 2
    public S_T_ItemGen ingredient;
    public int ingredientsToSpawn = 4;
    [System.NonSerialized] public int newID;
    public Sprite[] ingredients;
    [SerializeField] private Transform itemManager;
    public SpriteRenderer[] visuals;
    private int lastRawIngredient = 14;
    public float skill2Cooldown = 120f;
    public float skill2LastUsedTime = -Mathf.Infinity;

    //skill 3
    private float width;
    private float height;
    private Vector2 OldPos;
    private Vector2 NewPos;
    Collider2D hit;
    public float skill3Cooldown = 25f;
    public float skill3LastUsedTime = -Mathf.Infinity;

    //skill 4
    private int ingredientId;
    public SpriteRenderer heldItem;
    static public bool SkillUsed;
    public float skill4Cooldown = 300f;
    public float skill4LastUsedTime = -Mathf.Infinity;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        width = S_T_MazeGenerator.Instance.width * S_T_MazeGenerator.Instance.scale;
        height = S_T_MazeGenerator.Instance.height * S_T_MazeGenerator.Instance.scale;
        Skill1Unlocked = S_T_SkillUnlock.Skill1Unlocked;
        Skill2Unlocked = S_T_SkillUnlock.Skill2Unlocked;
        Skill3Unlocked = S_T_SkillUnlock.Skill3Unlocked;
        Skill4Unlocked = S_T_SkillUnlock.Skill4Unlocked;
    }

    private void Update()
    {
        if (!S_T_PauseMenu.isPaused)
        {
            // ===================================== SKILL 1 ======================================
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TryUseSkill1();
            }
            // ===================================== SKILL 2 ======================================
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryUseSkill2();
            }
            // ===================================== SKILL 3 ======================================
            if (Input.GetKeyDown(KeyCode.X))
            {
                TryUseSkill3(5f);
            }
            // ===================================== SKILL 4 ======================================
            if (Input.GetKeyDown(KeyCode.Z))
            {
                TryUseSkill4();
            }
            // ===================================== DEBUG BUTTON ======================================
            if (Input.GetKeyDown(KeyCode.Keypad7))
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
        m_MovementSpeed.SetSpeed(10f);
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
    private void Skill3(float range)
    {
        OldPos = m_Player.transform.position;
            NewPos = OldPos + S_T_PlayerMovement.lastMoveDirection * range;
            hit = Physics2D.OverlapPoint(NewPos);
            if (!(NewPos.x < 0 || NewPos.y < 0 || NewPos.x > width || NewPos.y > height))
            {
                if (hit != null && hit.CompareTag("Floor"))
                {
                    m_Player.transform.position = NewPos;
                    range = 5;
                    skill3LastUsedTime = Time.time;
                }
                else
                {
                    Skill3(range + 0.1f);
                }
        }
    }

    [Tooltip("Spawn one random item")]
    private void Skill4() //instantly process a held food item
    {
        // Increase time while cooking

        var ing = S_T_PlayerMovement.Instance.GrabLinkedItem(7);
        if (ing != null)
        {
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
                    skill4LastUsedTime = Time.time;
                    break;
                case (int)Food.Fish:
                    heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.CookedFish];
                    skill4LastUsedTime = Time.time;
                    break;
                case (int)Food.Meat:
                    heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.CookedMeat];
                    skill4LastUsedTime = Time.time;
                    break;
                case (int)Food.Egg:
                    heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.CookedEgg];
                    skill4LastUsedTime = Time.time;
                    break;
                case (int)Food.Cocoa:
                    heldItem.sprite = S_T_ItemManager.Instance.ingredients[(int)Food.Chocolate];
                    skill4LastUsedTime = Time.time;
                    break;
                default:
                    skill4LastUsedTime = -Mathf.Infinity;
                    Debug.Log("Failed to use skill");
                    break;
            }
            // Update the physical item
            ing.visuals[0].sprite = heldItem.sprite;
            ing.visuals[1].sprite = heldItem.sprite;
        }
    }


    private void TryUseSkill3(float range)
    {
        if (Skill3Unlocked)
        {
            if (Time.time - skill3LastUsedTime >= skill3Cooldown)
            {
                Skill3(range);
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
                Skill4();
            }
            else
            {
                Debug.Log("Skill 4 is on cooldown!");
            }
        }

    }
}