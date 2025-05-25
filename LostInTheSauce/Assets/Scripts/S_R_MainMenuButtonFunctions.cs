using UnityEngine;
using UnityEngine.SceneManagement;

public class S_R_MainMenuButtonFunctions : MonoBehaviour
{
    [SerializeField] GameObject m_camera;
    int m_moveDistance = 45;
    public void PlayButton()
    {
        SceneManager.LoadScene("Day1");
    }
    public void InstructionsButton()
    {
        m_camera.transform.position += new Vector3(m_moveDistance, 0, 0);
    }
    public void OptionButton()
    {
        m_camera.transform.position += new Vector3(-2* m_moveDistance, 0, 0);
    }
    public void CreditsButton()
    {
        m_camera.transform.position -= new Vector3(m_moveDistance, 0, 0);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void BackButton()
    {
        m_camera.transform.position = new Vector3(0, 0, -10);
    }
    public void BackButtonInstruction()
    {
        m_camera.transform.position -= new Vector3(m_moveDistance, 0, 0);
    }
}
