using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    [SerializeField]
    private bool canOpen;

    [SerializeField]
    private string[] itemsForSale = new string[14];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && Input.GetButtonDown("Fire1") && PlayerController.instance.CanMove && !Shop.instance.ShopMenu.activeInHierarchy)
        {
            Shop.instance.ItemsForSale = itemsForSale;

            Shop.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
        }    
    }
}
