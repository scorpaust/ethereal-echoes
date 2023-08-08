using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [SerializeField]
    private GameObject battleScene;

    [SerializeField]
    private Transform[] playerPositions;

    [SerializeField]
    private Transform[] enemyPositions;

    [SerializeField]
    private BattleChar[] playerPrefabs;

    [SerializeField]
    private BattleChar[] enemyPrefabs;

    [SerializeField]
    private GameObject uiButtonsHolder;

    [SerializeField]
    private BattleMove[] movesList;

    [SerializeField]
    private GameObject enemyAttackEffect;

    [SerializeField]
    private DamageNumber damageNumber;

    [SerializeField]
    private Text[] playerNames;

    [SerializeField]
    private Text[] playerHps;

    [SerializeField]
    private Text[] playerMps;

    [SerializeField]
    private GameObject targetMenu;

    public GameObject TargetMenu
    {
        get
        {
            return targetMenu;
        }
        set
        {
            targetMenu = value;
        }
    }

    public GameObject MagicMenu;

    public GameObject ItemMenu;

    [SerializeField]
    private BattleTargetButton[] targetButtons;

    [SerializeField]
    private BattleMagicSelect[] magicButtons;

    [SerializeField]
    private BattleItemSelect[] itemButtons;

    [SerializeField]
    private int chanceToFlee;

    private bool battleActive;

    public bool iteming = false;

    public List<BattleChar> activeBattleChars = new List<BattleChar>();

    public List<int> playersToHeal = new List<int>();

    public int CurrentTurn { get; private set; }

    [SerializeField]
    private BattleNotification battleNotice;
    
    public BattleNotification BattleNotice
    {
        get
        {
            return battleNotice;
        }

        private set { }
    }

    private bool turnWaiting;

	public string ItemName;

	// Start is called before the first frame update
	void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            BattleStart(new string[] { "Skeleton", "Orc" });
        }

        if (battleActive)
        {
            if (turnWaiting)
            {
                if (activeBattleChars[CurrentTurn].IsPlayer)
                {
                    uiButtonsHolder.SetActive(true);
                }
                else
                {
                    uiButtonsHolder.SetActive(false);

                    StartCoroutine(EnemyMoveCo());
                }
            }
        }
    }

    public void BattleStart(string[] enemiesToSpawn)
    {
        if (!battleActive)
        {
            battleActive = true;

            GameManager.instance.BattleActive = true;

            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);

            battleScene.SetActive(true);

            AudioManager.instance.PlayBgm(0);

            InitPlayers();

            InitEnemies(enemiesToSpawn);

			turnWaiting = true;

            CurrentTurn = Random.Range(0, activeBattleChars.Count);
		}
    }

    private void InitPlayers()
    {
        for (int i = 0; i < playerPositions.Length; i++)
        {
            if (GameManager.instance.PlayerStats[i].gameObject.activeInHierarchy)
            {
                for (int j = 0; j < playerPrefabs.Length; j++)
                {
                    if (playerPrefabs[j].CharName.Trim() == GameManager.instance.PlayerStats[i].CharName.Trim())
                    {
                        BattleChar newPlayer = Instantiate(playerPrefabs[j], playerPositions[i].position, playerPositions[i].rotation);

                        newPlayer.transform.parent = playerPositions[i];

                        activeBattleChars.Add(newPlayer);

                        playersToHeal.Add(j);

                        CharStats thePlayer = GameManager.instance.PlayerStats[i];

                        activeBattleChars[i].CurrentHP = thePlayer.CurrentHp;

                        activeBattleChars[i].MaxHp = thePlayer.MaxHp;

                        activeBattleChars[i].CurrentMp = thePlayer.CurrentMp;

                        activeBattleChars[i].MaxMp = thePlayer.MaxMp;

                        activeBattleChars[i].Strength = thePlayer.Strength;

                        activeBattleChars[i].Defence = thePlayer.Defence;

                        activeBattleChars[i].WeaponPower = thePlayer.WeaponPower;

                        activeBattleChars[i].ArmorPower = thePlayer.ArmorPower;

                        activeBattleChars[i].PlayerLevel = thePlayer.PlayerLevel;
                    }
                }
            }

        }

        turnWaiting = true;

        CurrentTurn = 0;

        UpdateUIStats();
    }

    private void InitEnemies(string[] enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            if (enemiesToSpawn[i] != "")
            {
                for (int j = 0; j < enemyPrefabs.Length; j++)
                {
                    if (enemyPrefabs[j].CharName == enemiesToSpawn[i])
                    {
                        BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[i].position, enemyPositions[i].rotation);

                        newEnemy.transform.parent = enemyPositions[i];

                        activeBattleChars.Add(newEnemy);
					}
                }
            }
        }
    }

    public void NextTurn()
    {
        CurrentTurn++;

        if (CurrentTurn >= activeBattleChars.Count)
        {
            CurrentTurn = 0;
        }

        turnWaiting = true;

        UpdateBattle();

        UpdateUIStats();
    }

    public void UpdateBattle()
    {
        bool allEnemiesDead = true;

        bool allPlayersDead = true;

        for (int i = 0; i < activeBattleChars.Count; i++)
        {
            if (activeBattleChars[i].CurrentHP < 0)
            {
                activeBattleChars[i].CurrentHP = 0;
            }

            if (activeBattleChars[i].CurrentHP == 0)
            {
                // Handle dead battler
            }
            else
            {
                if (activeBattleChars[i].IsPlayer)
                {
                    allPlayersDead = false;
                }
                else
                {
                    allEnemiesDead = false;
                }
            }
        }

        if (allEnemiesDead || allPlayersDead)
        {
            if (allEnemiesDead)
            {
                // End battle in victory
            }
            else
            {
                // End battle in failure
            }

            battleScene.SetActive(false);

            GameManager.instance.BattleActive = false;

            battleActive = false;
        }
        else
        {
            while (activeBattleChars[CurrentTurn].CurrentHP == 0)
            {
                CurrentTurn++;

                if (CurrentTurn >= activeBattleChars.Count)
                {
                    CurrentTurn = 0;
                }
            }
        }
    }

    private IEnumerator EnemyMoveCo()
    {
        turnWaiting = false;

        yield return new WaitForSeconds(1f);

        EnemyAttack();

		yield return new WaitForSeconds(1f);

        NextTurn();
	}

    public void EnemyAttack()
    {
        List<int> players = new List<int>();

        for (int i = 0; i < activeBattleChars.Count; i++)
        {
            if (activeBattleChars[i].IsPlayer && activeBattleChars[i].CurrentHP > 0)
            {
                players.Add(i);
            }
        }

        int selectedTarget = players[Random.Range(0, players.Count)];

        int selectedAttack = Random.Range(0, activeBattleChars[CurrentTurn].MovesAvailable.Length);

        int movePower = 0;

        for (int i = 0; i < movesList.Length; i++)
        {
            if (movesList[i].MoveName == activeBattleChars[CurrentTurn].MovesAvailable[selectedAttack]) 
            {
                Instantiate(movesList[i].TheEffect, activeBattleChars[selectedTarget].transform.position, activeBattleChars[selectedTarget].transform.rotation);

                movePower = movesList[i].MovePower;
            }
        }

        Instantiate(enemyAttackEffect, activeBattleChars[CurrentTurn].transform.position, activeBattleChars[CurrentTurn].transform.rotation);

        DealDamage(selectedTarget, movePower);
    }

    public void DealDamage(int target, int movePower)
    {
        if (!iteming)
        {
			// Base damage calculation
			int baseDamage = (activeBattleChars[CurrentTurn].Strength + activeBattleChars[CurrentTurn].WeaponPower) * activeBattleChars[CurrentTurn].PlayerLevel;

			// Defense mitigation calculation
			int defenseMitigation = (activeBattleChars[target].Defence + activeBattleChars[target].ArmorPower);

			// Randomness factor (between 0.85 and 1.15 here)
			float randomFactor = UnityEngine.Random.Range(0.85f, 1.15f);

			// Damage calculation after applying defense mitigation
			// We use Mathf.Max to make sure that the damage is at least 1, 
			// and doesn't go negative due to high defense.
			int finalDamage = Mathf.RoundToInt(Mathf.Max(1, baseDamage - defenseMitigation) * randomFactor);

			activeBattleChars[target].CurrentHP -= finalDamage;

			damageNumber.SetDamage(finalDamage);

			Instantiate(damageNumber, activeBattleChars[target].transform.position, activeBattleChars[target].transform.rotation);

			UpdateUIStats();
		}
		else
        {
            UpdatePlayerStats();
        }
    }

    public void UpdatePlayerStats()
    {
		for (int i = 0; i < playerNames.Length; i++)
		{
			if (activeBattleChars.Count > i)
			{
				if (activeBattleChars[i].IsPlayer)
				{
					BattleChar playerData = activeBattleChars[i];

					playerData.CurrentHP = GameManager.instance.PlayerStats[playersToHeal[i]].CurrentHp;

					playerData.CurrentMp = GameManager.instance.PlayerStats[playersToHeal[i]].CurrentMp;

					UpdateUIStats();
				}
			}
		}
	}

    public void UpdateUIStats()
    {
        for (int i = 0; i < playerNames.Length; i++)
        {
            if (activeBattleChars.Count > i)
            {
				if (activeBattleChars[i].IsPlayer)
                {
                    playerNames[i].gameObject.SetActive(true);

                    BattleChar playerData = activeBattleChars[i];

                    playerNames[i].text = playerData.CharName;

                    playerHps[i].text = playerData.CurrentHP + "/" + playerData.MaxHp;

                    playerMps[i].text = playerData.CurrentMp + "/" + playerData.MaxMp;
                }
                else
                {
					playerNames[i].gameObject.SetActive(false);
				}
			} 
            else
            {
				playerNames[i].gameObject.SetActive(false);
			}
        }
    }

    public void PlayerAttack(string moveName, int selectedTarget)
    {
 		int movePower = 0;

		for (int i = 0; i < movesList.Length; i++)
		{
			if (movesList[i].MoveName == moveName)
			{
				Instantiate(movesList[i].TheEffect, activeBattleChars[selectedTarget].transform.position, activeBattleChars[selectedTarget].transform.rotation);

				movePower = movesList[i].MovePower;
			}
		}

		Instantiate(enemyAttackEffect, activeBattleChars[CurrentTurn].transform.position, activeBattleChars[CurrentTurn].transform.rotation);

		DealDamage(selectedTarget, movePower);

        uiButtonsHolder.SetActive(false);

        targetMenu.SetActive(false);

        NextTurn();
	}

    public void OpenTargetMenu(string moveName)
    {
        targetMenu.SetActive(true);

        List<int> enemies = new List<int>();

        for (int i = 0; i < activeBattleChars.Count; i++)
        {
            if (!activeBattleChars[i].IsPlayer)
            {
                enemies.Add(i);
            }
        }

        for (int i = 0; i < targetButtons.Length; i++)
        {
            if (enemies.Count > i)
            {
                targetButtons[i].gameObject.SetActive(true);

                targetButtons[i].MoveName = moveName;

				targetButtons[i].ActiveBattlerTarget = enemies[i];

				targetButtons[i].TargetName.text = activeBattleChars[enemies[i]].CharName;
			}
            else
            {
				targetButtons[i].gameObject.SetActive(false);
			}
        }
    }

	public void OpenPlayersMenu()
	{
		targetMenu.SetActive(true);

        iteming = true;

		for (int i = 0; i < playersToHeal.Count; i++)
		{
			if (playersToHeal.Count > i)
			{
				targetButtons[i].gameObject.SetActive(true);

				targetButtons[i].ActiveBattlerTarget = playersToHeal[i];

                itemButtons[i].targetToHeal = playersToHeal[i];

				targetButtons[i].TargetName.text = activeBattleChars[playersToHeal[i]].CharName;
			}
			else
			{
				targetButtons[i].gameObject.SetActive(false);
			}
		}
	}

	public void OpenMagicMenu()
    {
        MagicMenu.SetActive(true);

        for (int i = 0; i < magicButtons.Length; i++)
        {
            if (activeBattleChars[CurrentTurn].MovesAvailable.Length > i)
            {
                magicButtons[i].gameObject.SetActive(true);

                magicButtons[i].SpellName = activeBattleChars[CurrentTurn].MovesAvailable[i];

                magicButtons[i].NameText.text = magicButtons[i].SpellName;

                for (int j= 0; j < movesList.Length; j++)
                {
                    if (movesList[j].MoveName == magicButtons[i].SpellName)
                    {
                        magicButtons[i].SpellCost = movesList[j].MoveCost;

                        magicButtons[i].CostText.text = magicButtons[i].SpellCost.ToString();
                    }
                }
            }
            else
            {
				magicButtons[i].gameObject.SetActive(false);
			}
        }
    }

    private bool canActivateButton(int button)
    {
        return GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[button]) && GameManager.instance.numberOfItems[button] > 0 && GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[button]).itemType == ItemType.Default;
	}

    private int activeItemButtons()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            if (itemButtons[i].gameObject.activeInHierarchy)
                return 1;
        }

        return 0;
    }

    public void OpenItemMenu()
    {
		ItemMenu.SetActive(true);

		for (int i = 0; i < itemButtons.Length; i++)
		{
			if (canActivateButton(i))
			{
				Debug.Log(i);

				itemButtons[i].gameObject.SetActive(true);

				itemButtons[i].ItemName = GameManager.instance.itemsHeld[i];

				itemButtons[i].ItemText.text = itemButtons[i].ItemName;

				itemButtons[i].ItemCount = GameManager.instance.numberOfItems[i];

				itemButtons[i].CountText.text = itemButtons[i].ItemCount.ToString();
			}
			else
			{
				itemButtons[i].gameObject.SetActive(false);

				if (activeItemButtons() == 0)
				{
					ItemMenu.SetActive(false);
					battleNotice.TheText.text = "No items available to use.";
					battleNotice.Activate();
				}
			}

		}

	}

	public void Flee()
    {
        int fleeSuccess = Random.Range(0, 100);

        if (fleeSuccess < chanceToFlee)
        {
            battleActive = false;

            battleScene.SetActive(false);
        }
        else
        {
            NextTurn();

            battleNotice.TheText.text = "Couldn't escape...";

            battleNotice.Activate();
        }
    }
}
