using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class S_T_PlayerMovement : MonoBehaviour
{
    public float speed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!S_T_PlayerVariables.isRunning)
        {
            speed = 3;
        }
        else if (S_T_PlayerVariables.isRunning)
        {
            speed = 7;
        }
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize(); // Makes diagonal movement the same speed as other movement

        S_T_PlayerVariables.isWalking = (input.x != 0 || input.y != 0);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }
}
