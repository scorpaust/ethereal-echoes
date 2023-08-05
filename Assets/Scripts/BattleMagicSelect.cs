using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagicSelect : MonoBehaviour
{
    private string spellName;

    public string SpellName
    {
        get
        {
            return spellName;
        }

        set
        {
            spellName = value;
        }
    }

    private int spellCost;

    public int SpellCost
    {
        get
        {
            return spellCost;
        }

        set
        {
            spellCost = value;
        }
    }

    [SerializeField]
    private Text nameText;

    public Text NameText
    {
        get
        {
            return nameText;
        }

        set
        {
            nameText = value;
        }
    }

    [SerializeField]
    private Text costText;

    public Text CostText
    {
        get
        {
            return costText;
        }

        set
        {
            costText = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // CheckMp();
	}

    public void Press()
    {
        if (BattleManager.instance.activeBattleChars[BattleManager.instance.CurrentTurn].CurrentMp >= spellCost)
        {
            BattleManager.instance.MagicMenu.SetActive(false);

			BattleManager.instance.OpenTargetMenu(spellName);

            BattleManager.instance.activeBattleChars[BattleManager.instance.CurrentTurn].CurrentMp -= spellCost;
		}
        else
        {
            BattleManager.instance.BattleNotice.TheText.text = "Not Enough MP";

            BattleManager.instance.BattleNotice.Activate();

            BattleManager.instance.MagicMenu.SetActive(false);
        }
    }

    private void CheckMp()
    {
		if (BattleManager.instance.activeBattleChars[BattleManager.instance.CurrentTurn].CurrentMp < spellCost)
		{
			gameObject.GetComponent<Button>().enabled = false;
		}
		else
		{
			gameObject.GetComponent<Button>().enabled = true;
		}
	}
}
