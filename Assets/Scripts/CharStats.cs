using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [SerializeField]
    private string charName;

    [SerializeField]
    private int playerLevel = 1;

    [SerializeField]
    private int currentXp;

    [SerializeField]
    private int[] xpToNextLevel;

    [SerializeField]
    private int baseXp = 1000;

    [SerializeField]
    private int xpIncrease = 65;

    [SerializeField]
    private int maxLevel = 99;

    [SerializeField]
    private int currentHp;

    [SerializeField]
    private int maxHp = 100;

    [SerializeField]
    private int currentMp;

    [SerializeField]
    private int maxMp = 100;

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

    // Start is called before the first frame update
    void Start()
    {
        xpToNextLevel = new int[maxLevel];

        xpToNextLevel[1] = baseXp;

        for (int i = 2; i < xpToNextLevel.Length; i++)
		{
            xpToNextLevel[i] = Mathf.FloorToInt(xpToNextLevel[i - 1] * 1.05f);
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
        currentXp += amountOfXpToAdd;

        if (playerLevel < maxLevel)
		{
            if (currentXp > xpToNextLevel[playerLevel])
            {
                currentXp -= xpToNextLevel[playerLevel];

                playerLevel++;

                if (playerLevel % 2 == 0)
                {
                    strength++;
                }
                else
                {
                    defence++;
                }

                maxHp += Mathf.FloorToInt(maxHp * 1.05f);

                currentHp = maxHp;

                maxMp += maxMp + mpLevelBonus[playerLevel];

                currentMp = maxMp;
            }
        }
        
        if (playerLevel >= maxLevel)
		{
            currentXp = 0;
		}
	}
}
