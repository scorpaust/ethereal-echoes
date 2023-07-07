using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Weapon,
    Armor
}

public enum ItemAffect
{
    HP,
    MP,
    Strength
}

public class Item : MonoBehaviour
{
    [Header("Item Details")]
    public ItemType itemType;

    public string itemName;

    public string description;

    public int itemValue;

    public Sprite itemSprite;

    public int amountToChange;

    public ItemAffect itemAffect;

    [Header("Weapon/Armour Details")]
    public int weaponStrength;

    public int armorStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int charToUseOn)
	{
        CharStats selectedChar = GameManager.instance.PlayerStats[charToUseOn];

        if (itemType == ItemType.Default)
		{
            if (itemAffect == ItemAffect.HP)
			{
                selectedChar.CurrentHp += amountToChange;

                if (selectedChar.CurrentHp > selectedChar.MaxHp)
                    selectedChar.CurrentHp = selectedChar.MaxHp;
			}

            else if (itemAffect == ItemAffect.MP)
			{
                selectedChar.CurrentMp += amountToChange;

                if (selectedChar.CurrentMp > selectedChar.MaxMp)
                    selectedChar.CurrentMp = selectedChar.MaxMp;
            }

            else
			{
                selectedChar.Strength += amountToChange;
			}
		}

        if (itemType == ItemType.Weapon)
		{
            if (selectedChar.EquippedWeaponName != "")
			{
                GameManager.instance.AddItem(selectedChar.EquippedWeaponName);
			}

            selectedChar.EquippedWeaponName = itemName;

            selectedChar.WeaponPower = weaponStrength;
		}

        if (itemType == ItemType.Armor)
		{
            if (selectedChar.EquippedArmorName != "")
            {
                GameManager.instance.AddItem(selectedChar.EquippedArmorName);
            }

            selectedChar.EquippedArmorName = itemName;

            selectedChar.ArmorPower = armorStrength;
        }

        GameManager.instance.RemoveItem(itemName);
	}
}
