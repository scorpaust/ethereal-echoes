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

	[SerializeField]
	private SpriteRenderer theSprite;

	public SpriteRenderer TheSprite
	{
		get
		{
			return theSprite;
		}

		private set { }
	}

	[SerializeField]
	private Sprite deadSprite;

	public Sprite DeadSprite
	{
		get
		{
			return deadSprite;
		}

		private set { }
	}

	[SerializeField]
	private Sprite aliveSprite;

	public Sprite AliveSprite
	{
		get
		{
			return aliveSprite;
		}

		private set { }
	}

	private bool shouldFade;

	private float fadeSpeed = 1f;

	public float FadeSpeed
	{
		get
		{
			return fadeSpeed;
		}

		private set { }
	}

	public void EnemyFade()
	{
		shouldFade = true;
	}

	private void Update()
	{
        if (shouldFade)
        {
			theSprite.color = new Color(Mathf.MoveTowards(theSprite.color.r, 1f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.g, 0f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.b, 0f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.a, 0f, fadeSpeed * Time.deltaTime));
        }

		if (theSprite.color.a == 0f)
		{
			gameObject.SetActive(false);
		}
    }
}
