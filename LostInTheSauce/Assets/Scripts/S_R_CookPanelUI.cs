using UnityEngine;

public class S_R_CookPanel : MonoBehaviour
{
    [SerializeField] private GameObject m_RecipeBookUI;

    private void Awake()
    {
        m_RecipeBookUI.SetActive(false);
    }
    void Update()
    {
        if (!S_T_PauseMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                m_RecipeBookUI.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                m_RecipeBookUI.SetActive(false);
            }
        }
    }
}
