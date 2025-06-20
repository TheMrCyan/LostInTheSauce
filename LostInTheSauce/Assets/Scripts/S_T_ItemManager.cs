using UnityEngine;

public enum Food
{
    // Raw ingredients
    Tomato = 0,
    Meat = 1,
    Dough = 2,
    Cheese = 3,
    Lettuce = 4,
    Fish = 5,
    Rice = 6,
    Sugar = 7,
    Cucumber = 8,
    Potato = 9,
    Egg = 10,
    Milk = 11,
    Oil = 12,
    Cocoa = 13,
    Garlic = 14,
    // Processed ingredients
    Bread = 15,
    CookedFish = 16,
    CookedMeat = 17,
    CookedEgg = 18,
    Chocolate = 19
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
