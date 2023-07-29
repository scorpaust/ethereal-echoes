using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string[] itemsHeld;

    public int[] numberOfItems;

    public Item[] referenceItems;

    private int currentGold;

    public int CurrentGold
	{
        get
		{
            return currentGold;
		}

        set
		{
            currentGold = value;
		}
	}

    [SerializeField]
    private CharStats[] playerStats;

    public CharStats[] PlayerStats 
    {
        get
		{
            return playerStats;
		}

        private set { }
    }

    private bool gameMenuOpen;

    public bool GameMenuOpen
	{
        get
		{
            return gameMenuOpen;
		}

        set
		{
            gameMenuOpen = value;
		}
	}

    private bool dialogActive;

    public bool DialogActive
	{
        get
		{
            return dialogActive;
		}

        set
		{
            dialogActive = value;
		}
	}

    private bool shopActive;

    public bool ShopActive
	{
        get
		{
            return shopActive;
		}

        set
		{
            shopActive = value;
		}
	}

    private bool battleActive;

	public bool BattleActive
	{
		get
		{
			return battleActive;
		}

		set
		{
			battleActive = value;
		}
	}

	private bool fadingBetweenAreas;

    public bool FadingBetweenAreas
	{
        get
		{
            return fadingBetweenAreas;
		}

        set
		{
            fadingBetweenAreas = value;
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance == null) return;

        if (gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive || battleActive)
		{
            PlayerController.instance.CanMove = false;
		}
        else
		{
            if (PlayerController.instance.CanMove == false)
                PlayerController.instance.CanMove = true;
		}
    }

    public Item GetItemDetails(string itemToGrab)
	{
        for (int i = 0; i < referenceItems.Length; i++)
		{
            if (referenceItems[i].itemName == itemToGrab)
			{
                return referenceItems[i];
			}
		}

        return null;
	}

    public void SortItems()
    {
        bool itemAfterSpace = true;

        while(itemAfterSpace)
		{
            itemAfterSpace = false;

            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];

                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];

                    numberOfItems[i + 1] = 0;

                    if (itemsHeld[i] != "")
					{
                        itemAfterSpace = true;
					}
                }
            }
        }
    }

    public void AddItem(string itemToAdd)
	{
        int newItemPosition = 0;

        bool foundSpace= false;

        for (int i = 0; i < itemsHeld.Length; i++)
		{
            if (itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
			{
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
			}
		}

        if (foundSpace)
		{
            bool itemExists = false;

            for (int i = 0; i < referenceItems.Length; i++)
			{
                if (referenceItems[i].itemName == itemToAdd)
				{
                    itemExists = true;

                    i = referenceItems.Length;
				}
			}

            if (itemExists)
			{
                itemsHeld[newItemPosition] = itemToAdd;

                numberOfItems[newItemPosition]++;
			}
            else
			{
                Debug.LogError(itemToAdd + " does not exist.");
			}
		}

        GameMenu.instance.ShowItems();
	}

    public void RemoveItem(string itemToRemove)
	{
        bool foundItem = false;

        int itemPosition = 0;

        for (int i = 0; i < itemsHeld.Length; i++)
		{
            if (itemsHeld[i] == itemToRemove)
			{
                foundItem = true;

                itemPosition = i;

                i = itemsHeld.Length;
			}
		}

        if (foundItem)
		{
            numberOfItems[itemPosition]--;
            
            if (numberOfItems[itemPosition] <= 0)
			{
                itemsHeld[itemPosition] = "";
			}

            GameMenu.instance.ShowItems();
		}
        else
		{
            Debug.LogError("Couldn't find " + itemToRemove);
		}
	}

    public void SaveData()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);

        PlayerPrefs.SetFloat("Player_Position_X", PlayerController.instance.transform.position.x);

        PlayerPrefs.SetFloat("Player_Position_Y", PlayerController.instance.transform.position.y);

        PlayerPrefs.SetFloat("Player_Position_Z", PlayerController.instance.transform.position.z);

        // Save character info

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_active", 0);
            }

            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_Level", playerStats[i].PlayerLevel);

            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_CurrentXp", playerStats[i].CurrentXp);

            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_CurrentHp", playerStats[i].CurrentHp);

            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_MaxHp", playerStats[i].MaxHp);

            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_CurrentMp", playerStats[i].CurrentMp);

            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_MaxMap", playerStats[i].MaxMp);

            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_Strength", playerStats[i].Strength);

            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_Defence", playerStats[i].Defence);

            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_WeaponPower", playerStats[i].WeaponPower);

            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_ArmorPower", playerStats[i].ArmorPower);

            PlayerPrefs.SetString("Player_" + playerStats[i].CharName + "_EquippedWeapon", playerStats[i].EquippedWeaponName);

            PlayerPrefs.SetString("Player_" + playerStats[i].CharName + "_EquippedArmor", playerStats[i].EquippedArmorName);
        }

        // Store the inventory data

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);

            PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);
        }

    }

    public void LoadData()
    {
        if (PlayerController.instance != null)
        {
			PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_X"), PlayerPrefs.GetFloat("Player_Position_Y"), PlayerPrefs.GetFloat("Player_Position_Z"));

			for (int i = 0; i < playerStats.Length; i++)
			{
				if (PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_active") == 0)
				{
					playerStats[i].gameObject.SetActive(false);
				}
				else
				{
					playerStats[i].gameObject.SetActive(true);
				}

				playerStats[i].PlayerLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_Level");

				playerStats[i].CurrentXp = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_CurrentXp");

				playerStats[i].CurrentHp = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_CurrentHp");

				playerStats[i].MaxHp = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_MaxHp");

				playerStats[i].CurrentMp = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_CurrentMp");

				playerStats[i].MaxMp = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_MaxMap");

				playerStats[i].Strength = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_Strength");

				playerStats[i].Defence = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_Defence");

				playerStats[i].WeaponPower = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_WeaponPower");

				playerStats[i].ArmorPower = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_ArmorPower");

				playerStats[i].EquippedWeaponName = PlayerPrefs.GetString("Player_" + playerStats[i].CharName + "_EquippedWeapon");

				playerStats[i].EquippedArmorName = PlayerPrefs.GetString("Player_" + playerStats[i].CharName + "_EquippedArmor");
			}

			for (int i = 0; i < itemsHeld.Length; i++)
			{
				itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);

				numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
			}
		}        
    }
}