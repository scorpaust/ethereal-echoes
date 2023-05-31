using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string[] itemsHeld;

    public int[] numberOfItems;

    public Item[] referenceItems;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenAreas)
		{
            PlayerController.instance.CanMove = false;
		}
        else
		{
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
}
