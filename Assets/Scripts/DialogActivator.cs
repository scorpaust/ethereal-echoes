using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    [SerializeField]
    private string[] lines;

    [SerializeField]
    private bool isPerson = true;

    private bool canActivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.DialogBox.activeInHierarchy)
		{
            DialogManager.instance.ShowDialog(lines, isPerson);
		}
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
	    if (other.CompareTag("Player"))
		{
            canActivate = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
        if (other.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
}