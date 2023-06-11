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

    [SerializeField]
    private GameObject[] statusButtons;

    [SerializeField]
    private Text statusName, statusHP, statusMP, statusStrength, statusDefence, statusWeaponEquipped, statusWeaponPower, statusArmorEquipped, statusArmorPower, statusExpToNextLevel;

    [SerializeField]
    private Image statusImage;

    [SerializeField]
    private ItemButton[] itemButtons;

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
        UpdateMainStats();

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

    public void OpenStatus()
	{
        UpdateMainStats();

        StatusChar(0);

        for (int i = 0; i < statusButtons.Length; i++)
		{
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);

            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].CharName;
		}
	}

    public void StatusChar(int selected)
	{
        statusName.text = playerStats[selected].CharName;

        statusHP.text = playerStats[selected].CurrentHp + "/" + playerStats[selected].MaxHp;

        statusMP.text = playerStats[selected].CurrentMp + "/" + playerStats[selected].MaxMp;

        statusStrength.text = playerStats[selected].Strength.ToString();

        statusDefence.text = playerStats[selected].Defence.ToString();

        if (playerStats[selected].EquippedWeaponName != "")
		{
            statusWeaponEquipped.text = playerStats[selected].EquippedWeaponName;
		}

        statusWeaponPower.text = playerStats[selected].WeaponPower.ToString();

        if (playerStats[selected].EquippedArmorName != "")
        {
            statusArmorEquipped.text = playerStats[selected].EquippedArmorName;
        }

        statusArmorPower.text = playerStats[selected].ArmorPower.ToString();

        statusExpToNextLevel.text = playerStats[selected].XpToNextLevel[playerStats[selected].PlayerLevel - playerStats[selected].CurrentXp].ToString();

        statusImage.sprite = playerStats[selected].CharImage;
    }

    public void ShowItems()
	{
        GameManager.instance.SortItems();

        for (int i = 0; i < itemButtons.Length; i++)
		{
            itemButtons[i].ButtonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
			{
                if (!itemButtons[i].ButtonImage.gameObject.activeInHierarchy)
                    itemButtons[i].ButtonImage.gameObject.SetActive(true);

                if (GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]))
                    itemButtons[i].ButtonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;

                itemButtons[i].AmountText.text = GameManager.instance.numberOfItems[i].ToString();
			}
            else
			{
                itemButtons[i].ButtonImage.gameObject.SetActive(false);

                itemButtons[i].AmountText.text = "";
			}
		}
	}
}
