using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private Text dialogText;

    [SerializeField]
    private Text nameText;

    [SerializeField]
    private GameObject dialogBox;

    public GameObject DialogBox
	{
        get
		{
            return dialogBox;
		}
	}

    [SerializeField]
    private GameObject nameBox;

    [SerializeField]
    private string[] dialogLines;

    private int currentLine;

    public static DialogManager instance;

    private bool justStarted;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        dialogText.text = dialogLines[currentLine];
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
		{
            if (Input.GetButtonUp("Fire1"))
			{
                if (!justStarted)
				{
                    currentLine++;

                    if (currentLine >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);

                        GameManager.instance.DialogActive = false;
                    }
                    else
                    {
                        CheckIfName();

                        dialogText.text = dialogLines[currentLine];
                    }
                }
                else
				{
                    justStarted = false;
				}
			}
		}
    }

    public void ShowDialog(string[] newLines, bool isPerson)
	{
        dialogLines = newLines;

        currentLine = 0;

        CheckIfName();

        dialogText.text = dialogLines[currentLine];

        dialogBox.SetActive(true);

        justStarted = true;

        nameBox.gameObject.SetActive(isPerson);

        GameManager.instance.DialogActive = true;
	}

    public void CheckIfName()
	{
        if (dialogLines[currentLine].StartsWith("n*"))
		{
            nameText.text = dialogLines[currentLine].Replace("n*", "");

            currentLine++;
		}
	}
}
