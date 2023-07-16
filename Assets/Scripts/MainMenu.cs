using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string startingScene;

    [SerializeField]
    private GameObject continueButton;

    [SerializeField]
    private string loadGameScene;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Current_Scene"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        SceneManager.LoadScene(loadGameScene);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(startingScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
