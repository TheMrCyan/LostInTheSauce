using UnityEngine;

public class S_T_PlayerSprint : MonoBehaviour
{
    public float totalStamina;
    public float stamina;
    public GameObject staminaBar;

    void Awake()
    {
        stamina = totalStamina;
    }

    void Update()
    {
        // Player starts sprinting when pressing shift, stamina depletes
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && S_T_PlayerVariables.isWalking)
        {
            S_T_PlayerVariables.isRunning = true;
            stamina -= 0.2f;
        }
        else
        {
            S_T_PlayerVariables.isRunning = false;
        }

        // Stamina replenishes while standing still
        if (stamina < 100 && !S_T_PlayerVariables.isWalking)
        {
            stamina += 0.1f;
        }

        // Adjusts the stamina bar to represent current stamina
        if (staminaBar != null)
        {
            staminaBar.transform.localScale = new Vector2(stamina / totalStamina, staminaBar.transform.localScale.y);
        }
    }
}
