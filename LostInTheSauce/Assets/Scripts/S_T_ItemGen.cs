using UnityEngine;

public class S_T_ItemGen : MonoBehaviour
{
    private bool touchingPlayer;
    public int id;
    public SpriteRenderer visuals;

    void Awake()
    {
        visuals = GetComponentInChildren<SpriteRenderer>(true);
    }

    void Update()
    {
        if (transform.position == Vector3.zero && S_T_MazeGenerator.Instance.validItemSpaces != null)
        {
            RandomizeLocation();
        }

        if (touchingPlayer && Input.GetKey(KeyCode.Space) && S_T_PlayerMovement.Instance.heldItem.sprite == null)
        {
            // Update ID to not override other spawned pickups
            id = S_T_ItemManager.Instance.newID;
            S_T_ItemManager.Instance.newID += 1;

            // Hold pickup above player
            S_T_PlayerMovement.Instance.heldItem.sprite = visuals.sprite;

            RandomizeLocation();
        }
    }

    private void RandomizeLocation()
    {
        visuals.sprite = S_T_ItemManager.Instance.ingredients[Random.Range(0, S_T_ItemManager.Instance.ingredients.Length)];
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
    }
}
