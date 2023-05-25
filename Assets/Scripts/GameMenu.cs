using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;

    private CharStats[] playerStats;

    [SerializeField]
    private GameObject[] windows;

    [SerializeField]
    private Text[] nameText;

    [SerializeField]
    private Text[] hpText;

    [SerializeField]
    private Text[] mpText;

    [SerializeField]
    private Text[] levelText;

    [SerializeField]
    private Text[] expText;

    [SerializeField]
    private Slider[] expSlider;

    [SerializeField]
    private Image[] charImage;

    [SerializeField]
    private GameObject[] charStatsHolder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
		{
            if (theMenu.activeInHierarchy)
			{
                CloseMenu();
			}
            else
			{
                theMenu.SetActive(true);

                UpdateMainStats();

                GameManager.instance.GameMenuOpen = true;
			}
		}
    }

    public void UpdateMainStats()
	{
        playerStats = GameManager.instance.PlayerStats;

        for (int i = 0; i < playerStats.Length; i++)
		{
            if (playerStats[i].gameObject.activeInHierarchy)
			{
                charStatsHolder[i].SetActive(true);

                nameText[i].text = playerStats[i].CharName;

                hpText[i].text = "HP: " + playerStats[i].CurrentHp + "/" + playerStats[i].MaxHp;

                mpText[i].text = "MP: " + playerStats[i].CurrentMp + "/" + playerStats[i].MaxMp;

                expText[i].text = playerStats[i].CurrentXp + "/" + playerStats[i].XpToNextLevel[playerStats[i].PlayerLevel];

                expSlider[i].maxValue = playerStats[i].XpToNextLevel[playerStats[i].PlayerLevel];

                expSlider[i].value = playerStats[i].CurrentXp;

                charImage[i].sprite = playerStats[i].CharImage;
            }
            else
			{
                charStatsHolder[i].SetActive(false);
			}
		}
	}

    public void ToggleWindow(int windowNumber)
	{
        for (int i = 0; i < windows.Length; i++)
		{
            if (i == windowNumber)
			{
                windows[i].SetActive(!windows[i].activeInHierarchy);
			}
            else
			{
                windows[i].SetActive(false);
			}
		}
	}

    public void CloseMenu()
	{
        for (int i = 0; i < windows.Length; i++)
		{
            windows[i].SetActive(false);
		}

        theMenu.SetActive(false);

        GameManager.instance.GameMenuOpen = false;
	}
}
