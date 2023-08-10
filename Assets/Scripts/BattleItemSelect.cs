using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class BattleItemSelect : MonoBehaviour
{
	private string itemName;

	public string ItemName
	{
		get
		{
			return itemName;
		}

		set
		{
			itemName = value;
		}
	}

	private int itemCount;

	public int ItemCount
	{
		get
		{
			return itemCount;
		}

		set
		{
			itemCount = value;
		}
	}

	[SerializeField]
	private Text itemText;

	public Text ItemText
	{
		get
		{
			return itemText;
		}

		set
		{
			itemText = value;
		}
	}

	[SerializeField]
	private Text countText;

	public Text CountText
	{
		get
		{
			return countText;
		}

		set
		{
			countText = value;
		}
	}

	public int targetToHeal;

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
		if (GameManager.instance.itemsHeld.Contains(itemName))
		{
			BattleManager.instance.ItemMenu.SetActive(false);

			BattleManager.instance.OpenPlayersMenu();

			BattleManager.instance.ItemName = itemName;
		}
		else
		{
			itemText.text = itemName;

			itemCount = 0;

			countText.text = itemCount.ToString();

			BattleManager.instance.BattleNotice.TheText.text = "Not Enough " + itemName + "'s.";

			BattleManager.instance.BattleNotice.Activate();

			BattleManager.instance.ItemMenu.SetActive(false);
		}
	}
}
