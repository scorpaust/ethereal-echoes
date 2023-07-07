using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    [SerializeField]
    private GameObject shopMenu;

    public GameObject ShopMenu 
    {
        get {
            return shopMenu;
        }

        private set {}
    }

    [SerializeField]
    private GameObject buyMenu;

    public GameObject BuyMenu 
    {
        get {
            return buyMenu;
        }

        private set {}
    }

    [SerializeField]
    private GameObject sellMenu;

    public GameObject SellMenu 
    {
        get {
            return sellMenu;
        }

        private set {}
    }

    [SerializeField]
    private Text goldText;

    [SerializeField]
    private string[] itemsForSale;

    public string[] ItemsForSale 
    {
        get {
            return itemsForSale;
        }

        set {
            itemsForSale = value;
        }
    }

    [SerializeField]
    private ItemButton[] buyItemButtons;

    [SerializeField]
    private ItemButton[] sellItemButtons;

    [SerializeField]
    private Item selectedItem;

    [SerializeField]
    private Text buyItemName;

    [SerializeField]    
    private Text buyItemDescription;

    [SerializeField]
    private Text buyItemValue;

    [SerializeField]
    private Text sellItemName;

    [SerializeField]
    private Text sellItemDescription;

    [SerializeField]
    private Text sellItemValue;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenShop()
	{
        shopMenu.SetActive(true);

        OpenBuyMenu();

        GameManager.instance.ShopActive = true;

        goldText.text = GameManager.instance.CurrentGold.ToString();
	}

    public void CloseShop()

	{
        shopMenu.SetActive(false);

        GameManager.instance.ShopActive = false;
    }

    public void OpenBuyMenu()
	{
        buyItemButtons[0].Press();

        buyMenu.SetActive(true);

        sellMenu.SetActive(false);

        GameManager.instance.SortItems();

        for (int i = 0; i < buyItemButtons.Length; i++)
		{
            buyItemButtons[i].ButtonValue = i;

            if (itemsForSale[i] != "")
			{
   
                buyItemButtons[i].ButtonImage.gameObject.SetActive(true);
             
                buyItemButtons[i].ButtonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;

                buyItemButtons[i].AmountText.text = "";
			}
            else
			{
                buyItemButtons[i].ButtonImage.gameObject.SetActive(false);

                buyItemButtons[i].AmountText.text = "";
			}
		}
	}

    public void OpenSellMenu()
	{
        sellItemButtons[0].Press();

        buyMenu.SetActive(false);

        sellMenu.SetActive(true);

        GameManager.instance.SortItems();

        ShowSellItems();
    }

    public void SelectBuyItem(Item buyItem)
    {
        GameManager.instance.SortItems();

        selectedItem = buyItem;

        if (selectedItem)
        {
            buyItemName.text = selectedItem.itemName;

            buyItemDescription.text = selectedItem.description;

            Debug.Log($"Value: {selectedItem.itemValue}g");

            buyItemValue.text = $"Value: {selectedItem.itemValue}g";
        }       
    }

    public void SelectSellItem(Item sellItem) 
    {
        GameManager.instance.SortItems();

        selectedItem = sellItem;

        if (selectedItem)
        {
            sellItemName.text = selectedItem.itemName;

            sellItemDescription.text = selectedItem.description;

            sellItemValue.text ="Value: " + Mathf.FloorToInt(selectedItem.itemValue * .5f).ToString() + "g";
        }
    }

    public void BuyItem()
    {
        if (selectedItem != null) {
            if (GameManager.instance.CurrentGold >= selectedItem.itemValue)
            {
                GameManager.instance.CurrentGold -= selectedItem.itemValue;

                GameManager.instance.AddItem(selectedItem.itemName);
            }

            goldText.text = GameManager.instance.CurrentGold.ToString() + "g";
        }
    }

    private void ShowSellItems() 
    {
        for (int i = 0; i < sellItemButtons.Length; i++)
		{
            sellItemButtons[i].ButtonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
			{
                sellItemButtons[i].ButtonImage.gameObject.SetActive(true);

                sellItemButtons[i].ButtonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;

                sellItemButtons[i].AmountText.text = GameManager.instance.numberOfItems[i].ToString();
			}
            else
			{
                sellItemButtons[i].ButtonImage.gameObject.SetActive(false);

                sellItemButtons[i].AmountText.text = "";
			}
		}
    }

    public void SellItem()
    {
        if (selectedItem != null)
        {
            GameManager.instance.CurrentGold += Mathf.FloorToInt(selectedItem.itemValue * .5f);

            GameManager.instance.RemoveItem(selectedItem.itemName);
        }

        goldText.text = GameManager.instance.CurrentGold.ToString() + "g";

        ShowSellItems();
    }
}
