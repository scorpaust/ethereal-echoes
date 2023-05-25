using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [SerializeField]
    private string charName;

    public string CharName
	{
        get
		{
            return charName;
		}

        set
		{
            charName = value;
		}
	}

    [SerializeField]
    private int playerLevel = 1;

    public int PlayerLevel { get => playerLevel; set => playerLevel = value; }

    [SerializeField]
    private int currentXp;

    public int CurrentXp { get => currentXp; set => currentXp = value; }

    [SerializeField]
    private int[] xpToNextLevel;

    public int[] XpToNextLevel { get => xpToNextLevel; set => xpToNextLevel = value; }

    [SerializeField]
    private int baseXp = 1000;

    [SerializeField]
    private int xpIncrease = 65;

    public int XpIncrease { get => xpIncrease; set => xpIncrease = value; }

    [SerializeField]
    private int maxLevel = 99;

    [SerializeField]
    private int currentHp;

    public int CurrentHp
	{
        get
		{
            return currentHp;
		}
        set
		{
            currentHp = value;
		}
	}

    [SerializeField]
    private int maxHp = 100;

    public int MaxHp
	{
        get
		{
            return maxHp;
		}

        set
		{
            maxHp = value;
		}
	}

    [SerializeField]
    private int currentMp;

    public int CurrentMp
	{
        get
		{
            return currentMp;
		}
        set
		{
            currentMp = value;
		}
	}

    [SerializeField]
    private int maxMp = 100;

    public int MaxMp
	{
        get
		{
            return maxMp;
		}
        set
		{
            maxMp = value;
		}
	}

	[SerializeField]
    private int[] mpLevelBonus;

    [SerializeField]
    private int strength;

    [SerializeField]
    private int defence;

    [SerializeField]
    private int weaponPower;

    [SerializeField]
    private int armorPower;

    [SerializeField]
    private string equippedWeaponName;

    [SerializeField]
    private string equippedArmorName;

    [SerializeField]
    private Sprite charImage;

    public Sprite CharImage { get => charImage; set => charImage = value; }

    // Start is called before the first frame update
    void Start()
    {
        XpToNextLevel = new int[maxLevel];

        XpToNextLevel[1] = baseXp;

        for (int i = 2; i < XpToNextLevel.Length; i++)
		{
            XpToNextLevel[i] = Mathf.FloorToInt(XpToNextLevel[i - 1] * 1.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
		{
            AddXp(500);
		}
    }

    public void AddXp(int amountOfXpToAdd)
	{
        CurrentXp += amountOfXpToAdd;

        if (PlayerLevel < maxLevel)
		{
            if (CurrentXp > XpToNextLevel[PlayerLevel])
            {
                CurrentXp -= XpToNextLevel[PlayerLevel];

                PlayerLevel++;

                if (PlayerLevel % 2 == 0)
                {
                    strength++;
                }
                else
                {
                    defence++;
                }

                maxHp += Mathf.FloorToInt(maxHp * 1.05f);

                currentHp = maxHp;

                maxMp += maxMp + mpLevelBonus[PlayerLevel];

                currentMp = maxMp;
            }
        }
        
        if (PlayerLevel >= maxLevel)
		{
            CurrentXp = 0;
		}
	}
}
