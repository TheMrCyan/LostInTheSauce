using UnityEngine;

public class S_T_Trash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        S_T_PlayerMovement.Instance.touchingTrash = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        S_T_PlayerMovement.Instance.touchingTrash = false;
    }
}
