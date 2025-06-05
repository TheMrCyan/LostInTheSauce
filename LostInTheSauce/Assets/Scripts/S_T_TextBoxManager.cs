using TMPro;
using UnityEngine;

public class S_T_TextBoxManager : MonoBehaviour
{
    public static S_T_TextBoxManager Instance { get; private set; }

    public GameObject textBox;
    public TMP_Text text;
    public TextAsset textFile;
    public string[] textLines;
    public int currentLine;
    public int endAtLine;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
    }
    private void Update()
    {
        text.text = textLines[currentLine];

        if (currentLine >= endAtLine)
        {
            textBox.SetActive(false);
        }
        else
        {
            if (S_T_Tutorial.Instance.canEnter)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    currentLine += 1;
                }
            }
        }
    }
}
