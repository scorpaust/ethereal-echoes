using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private string selectedItem;

    private Item activeItem;

    [SerializeField]
    private Text itemName;

    [SerializeField]
    private Text itemDescription;

    [SerializeField]
    private Text useButtonText;

    [SerializeField]
    private GameObject itemCharChoiceMenu;

    [SerializeField]
    private Text[] itemCharChoiceNames;

    public static GameMenu instance;

    [SerializeField]
    private Text goldText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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

            AudioManager.instance.PlaySFX(5);
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

        goldText.text = GameManager.instance.CurrentGold.ToString() + "g";
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

        itemCharChoiceMenu.SetActive(false);
	}

    public void CloseMenu()
	{
        for (int i = 0; i < windows.Length; i++)
		{
            windows[i].SetActive(false);
		}

        theMenu.SetActive(false);

        GameManager.instance.GameMenuOpen = false;

        itemCharChoiceMenu.SetActive(false);
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

    public void SelectItem(Item newItem)
	{
        activeItem = newItem;

        if (activeItem.itemType == ItemType.Default)
		{
            useButtonText.text = "Use";
		}
        else
		{
            useButtonText.text = "Equip";
		}

        itemName.text = activeItem.itemName;

        itemDescription.text = activeItem.description;
	}

    public void DiscardItem()
	{
        if(activeItem != null)
		{
            GameManager.instance.RemoveItem(activeItem.itemName);
		}
	}

    public void OpenItemCharChoice()
	{
        itemCharChoiceMenu.SetActive(true);

        for (int i = 0; i < itemCharChoiceNames.Length; i++)
		{
            itemCharChoiceNames[i].text = GameManager.instance.PlayerStats[i].CharName;

            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.PlayerStats[i].gameObject.activeInHierarchy);
		}
	}

    public void CloseItemCharChoice()
	{
        itemCharChoiceMenu.SetActive(false);
	}

    public void UseItem(int selectChar)
	{
        activeItem.Use(selectChar);

        CloseItemCharChoice();
	}

    public void SaveGame()
    {
        GameManager.instance.SaveData();

        QuestManager.instance.SaveQuestData();
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFX(4);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");

        Destroy(GameManager.instance.gameObject);

        Destroy(PlayerController.instance.gameObject);

        Destroy(AudioManager.instance.gameObject);

        Destroy(gameObject);
    }
}
