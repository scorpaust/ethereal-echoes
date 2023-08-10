using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject UIScreen;

	[SerializeField]
    private GameObject player;

    public GameObject Player
    {
        get {
            return player;
        }

        private set {}
    }

	[SerializeField]
    private GameObject gameManager;

    [SerializeField]
    private GameObject audioManager;

    [SerializeField]
    private GameObject battleManager;

    public static EssentialsLoader instance;

    // Start is called before the first frame update
    void Start()
    {
        CheckEventsSystem();

        instance = this;

        if (UIFade.instance == null)
		{
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
		}
        
        if (PlayerController.instance == null)
		{
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();

            PlayerController.instance = clone;
		}

        if (BattleManager.instance == null)
        {
            Instantiate(battleManager);
        }

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

    private void CheckEventsSystem()
    {
        EventSystem[] eventsSystem = GameObject.FindObjectsOfType<EventSystem>();

        for (int i = 1; i < eventsSystem.Length; i++)
        {
            Destroy(eventsSystem[i].gameObject);
        }
    }
}
