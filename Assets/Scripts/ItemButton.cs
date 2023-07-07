using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField]
    private Image buttonImage;

    public Image ButtonImage
	{
        get
		{
            return buttonImage;
		}

        set
		{
            buttonImage = value;
		}
	}

    [SerializeField]
    private Text amountText;

    public Text AmountText
	{
        get
		{
            return amountText;
		}

        set
		{
            amountText = value;
		}
	}

    [SerializeField]
    private int buttonValue;

    public int ButtonValue
	{
        get
		{
            return buttonValue;
		}

        set
		{
            buttonValue = value;
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
        if (GameMenu.instance.theMenu.activeInHierarchy)
        {
            if (GameManager.instance.itemsHeld[buttonValue] != "")
		    {
                GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
		    }
        }

        if (Shop.instance.ShopMenu.activeInHierarchy) 
        {
            if (Shop.instance.BuyMenu.activeInHierarchy)
            {
                Shop.instance.SelectBuyItem(GameManager.instance.GetItemDetails(Shop.instance.ItemsForSale[buttonValue]));
            }

            if (Shop.instance.SellMenu.activeInHierarchy)
            {
                Shop.instance.SelectSellItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
            }
        }

	}
}
