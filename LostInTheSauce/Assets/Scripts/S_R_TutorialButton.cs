using UnityEngine;

public class S_R_TutorialButton : MonoBehaviour
{
    [SerializeField] private GameObject TutorialInfo;
  public void HideInfo()
    {
       GameObject.Destroy(TutorialInfo);
    }
}
