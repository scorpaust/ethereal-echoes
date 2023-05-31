using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Weapon,
    Armour
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

    public int value;

    public Sprite itemSprite;

    public int amountToChange;

    public ItemAffect itemAffect;

    [Header("Weapon/Armour Details")]
    public int weaponStrength;

    public int armourStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
