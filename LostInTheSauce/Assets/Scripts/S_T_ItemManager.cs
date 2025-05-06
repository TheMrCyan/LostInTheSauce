using UnityEngine;
using UnityEngine.UIElements;

public enum Food
{
    Tomato = 0,
    Meat = 1,
    Dough = 2
}

public class S_T_ItemManager : MonoBehaviour
{
    public static S_T_ItemManager Instance { get; private set; }

    public S_T_ItemGen ingredient;
    public int ingredientsToSpawn = 4;
    [System.NonSerialized] public int newID;
    public Sprite[] ingredients;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < ingredientsToSpawn; i++)
        {
            S_T_ItemGen ing = Instantiate(ingredient, Vector3.zero, Quaternion.identity, transform);
            ing.id = i;
        }
        newID = ingredientsToSpawn;
    }
}
