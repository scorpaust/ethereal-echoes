using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTargetButton : MonoBehaviour
{
	[SerializeField]
	private GameObject healAmountText;

	[SerializeField]
    private string moveName;

    public string MoveName
    {
        get
        {
            return moveName;
        }

        set
        {
            moveName = value;
        }
    }

    [SerializeField]
    private int activeBattlerTarget;

    public int ActiveBattlerTarget
    {
        get
        {
            return activeBattlerTarget;
        }

        set
        {
            activeBattlerTarget = value;
        }
    }

    [SerializeField]
    private Text targetName;

    public Text TargetName
    {
        get
        {
            return targetName;
        }

        set
        {
            targetName = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        if (BattleManager.instance.iteming)
        {
            healAmountText.gameObject.GetComponentInChildren<Text>().text = GameManager.instance.GetItemDetails(BattleManager.instance.ItemName).amountToChange.ToString();

            Instantiate(healAmountText, BattleManager.instance.activeBattleChars[activeBattlerTarget].transform.position, BattleManager.instance.activeBattleChars[BattleManager.instance.CurrentTurn].transform.rotation);

			GameManager.instance.GetItemDetails(BattleManager.instance.ItemName).Use(activeBattlerTarget);

			BattleManager.instance.UpdatePlayerStats();

			BattleManager.instance.UpdateGameManagerPlayerStats(BattleManager.instance.CurrentTurn);

			BattleManager.instance.TargetMenu.SetActive(false);

			BattleManager.instance.iteming = false;

			BattleManager.instance.NextTurn();
		}
        else
        {
			BattleManager.instance.PlayerAttack(moveName, activeBattlerTarget);
		}
    }
}
