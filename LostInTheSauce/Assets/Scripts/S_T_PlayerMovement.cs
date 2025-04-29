using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class S_T_PlayerMovement : MonoBehaviour
{
    public float speed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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

        if (input.x != 0 || input.y != 0)
        {
            S_T_PlayerVariables.isWalking = true;
        }
        else
        {
            S_T_PlayerVariables.isWalking = false;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }
}
