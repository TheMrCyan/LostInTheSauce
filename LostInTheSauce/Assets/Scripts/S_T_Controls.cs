using UnityEngine;

public class S_T_Controls : MonoBehaviour
{
    public GameObject controlPanel;


    void Start()
    {
        controlPanel.SetActive(false);
    }

    void Update()
    {
        if (!S_T_PauseMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                controlPanel.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                controlPanel.SetActive(false);
            }
        }
    }
}
