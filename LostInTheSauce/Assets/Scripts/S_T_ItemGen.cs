using UnityEngine;

public class S_T_ItemGen : MonoBehaviour
{
    public bool held;
    private bool touchingPlayer;
    private int lastRawIngredient = 14;
    public int id;
    public SpriteRenderer[] visuals;

    void Awake()
    {
        visuals = GetComponentsInChildren<SpriteRenderer>(true);
    }

    void Update()
    {
        if (transform.position == Vector3.zero && S_T_MazeGenerator.Instance.validItemSpaces != null)
        {
            // Initial spawn spot if placed in the world manually
            RandomizeLocation();
        }

        if (touchingPlayer && Input.GetKeyDown(KeyCode.Space) && S_T_PlayerMovement.Instance.heldItem.sprite == null && !held)
        {
            // Hold pickup above player
            S_T_PlayerMovement.Instance.heldItem.sprite = visuals[0].sprite;

            held = true;
            S_T_Fridge.Instance.playerHolding = this;
            tag = "Untagged";
            S_T_PlayerMovement.Instance.touchingFloorItem = false;
            visuals[0].enabled = false;
            visuals[1].enabled = false;
        }
        else if (held && Input.GetKeyDown(KeyCode.Space) && !S_T_PlayerMovement.Instance.touchingFloorItem) // Drop item
        {
            LayDown();

            // Throw item away
            if (S_T_PlayerMovement.Instance.touchingTrash)
            {
                RandomizeLocation();
            }
            // Stove interaction
            else if (S_T_Stove.Instance.touchingPlayer)
            {
                StoreLinkedItem(0);
            }
        }
    }

    public void RandomizeLocation()
    {
        // Update ID to not override other spawned pickups
        id = S_T_ItemManager.Instance.newID;
        S_T_ItemManager.Instance.newID += 1;

        visuals[0].sprite = S_T_ItemManager.Instance.ingredients[Random.Range(0, lastRawIngredient + 1)];
        visuals[1].sprite = visuals[0].sprite; // Minimap
        transform.position = S_T_MazeGenerator.Instance.validItemSpaces[Random.Range(0, S_T_MazeGenerator.Instance.validItemSpaces.Count)] * S_T_MazeGenerator.Instance.scale;
        // Only spawn outside of player view
        if (Vector2.Distance(transform.position, S_T_PlayerMovement.Instance.transform.position) < 22f)
        {
            RandomizeLocation();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            var ing = collision.gameObject.GetComponent<S_T_ItemGen>();
            if (id > ing.id)
            {
                RandomizeLocation();
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            var ing = collision.gameObject.GetComponent<S_T_ItemGen>();
            if (id > ing.id)
            {
                RandomizeLocation();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
    }

    public void StoreLinkedItem(int id)
    {
        transform.position = new Vector2(1000 + id * 2, 0);
    }

    public void LayDown()
    {
        held = false;
        tag = "Pickup";
        visuals[0].enabled = true;
        visuals[1].enabled = true;
        transform.position = S_T_PlayerMovement.Instance.transform.position;
    }
}
