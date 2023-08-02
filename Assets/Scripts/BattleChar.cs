using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BattleChar : MonoBehaviour
{
	[SerializeField]
	private bool isPlayer;

	public bool IsPlayer
	{
		get
		{
			return isPlayer;
		}

		private set { }
	}

	[SerializeField]
	private string[] movesAvailable;

	public string[] MovesAvailable
	{
		get
		{
			return movesAvailable;
		}

		private set { }
	}

    [SerializeField]
    private bool hasDied;

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
	private int playerLevel;

	public int PlayerLevel
	{
		get
		{
			return playerLevel;
		}

		set
		{
			playerLevel = value;
		}
	}

    [SerializeField]
    private int currentXp;

    [SerializeField]
    private int currentHP;

    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = value;
        }
    }

    [SerializeField]
    private int maxHp;

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
    private int maxMp;

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
    private int strength;

	public int Strength
	{
		get
		{
			return strength;
		}

		set
		{
			strength = value;
		}
	}

	[SerializeField]
    private int defence;

	public int Defence
	{
		get
		{
			return defence;
		}

		set
		{
			defence = value;
		}
	}

	[SerializeField]
    private int weaponPower;

	public int WeaponPower
	{
		get
		{
			return weaponPower;
		}

		set
		{
			weaponPower = value;
		}
	}

	[SerializeField]
    private int armorPower;

	public int ArmorPower
	{
		get
		{
			return armorPower;
		}

		set
		{
			armorPower = value;
		}
	}

}
