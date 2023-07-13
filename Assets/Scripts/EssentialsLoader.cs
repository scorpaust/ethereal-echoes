using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject UIScreen;

    /*[SerializeField]
    private GameObject player;

    public GameObject Player
    {
        get {
            return player;
        }

        private set {}
    }*/

    [SerializeField]
    private GameObject gameManager;

    [SerializeField]
    private AudioManager audioManager;

    public static EssentialsLoader instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        if (UIFade.instance == null)
		{
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
		}
        /*
        if (PlayerController.instance == null)
		{
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();

            PlayerController.instance = clone;
		}*/

        if (AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }

        if (GameManager.instance == null)
		{
            Instantiate(gameManager);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
