using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject UIScreen;

    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (UIFade.instance == null)
		{
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
		}

        if (PlayerController.instance == null)
		{
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();

            PlayerController.instance = clone;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
