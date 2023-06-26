using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    [SerializeField]
    private GameObject shopMenu;

    [SerializeField]
    private GameObject buyMenu;

    [SerializeField]
    private GameObject sellMenu;

    [SerializeField]
    private Text goldText;

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

        GameManager.instance.ShopActive = true;
	}

    public void CloseShop()

	{
        shopMenu.SetActive(false);

        GameManager.instance.ShopActive = false;
    }

    public void OpenBuyMenu()
	{
        buyMenu.SetActive(true);

        sellMenu.SetActive(false);

        goldText.text = GameManager.instance.CurrentGold.ToString();
	}

    public void OpenSellMenu()
	{
        buyMenu.SetActive(false);

        sellMenu.SetActive(true);
    }
}
