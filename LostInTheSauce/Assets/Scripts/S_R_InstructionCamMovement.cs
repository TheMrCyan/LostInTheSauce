using UnityEngine;
using UnityEngine.SceneManagement;

public class S_R_InstructionCamMovement : MonoBehaviour
{
    [SerializeField] GameObject m_camera;
   int m_moveDistance = 50;
   public void NextButton()
    {
        m_camera.transform.position += new Vector3(m_moveDistance, 0f, 0f);
    }
    public void StartDay1()
    {
        SceneManager.LoadScene("Day1");
    }
}
