using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private string mainMenuScene;

    [SerializeField]
    private string loadGameScene;

    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.instance != null)
			AudioManager.instance.PlayBgm(4);
		
        if (PlayerController.instance != null)
			PlayerController.instance.gameObject.SetActive(false);

		if (GameMenu.instance != null)
            GameMenu.instance.gameObject.SetActive(false);

        if (BattleManager.instance != null)
            BattleManager.instance.gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update()
    {
	}

    public void QuitToMainMenu()
    {
        Destroy(GameManager.instance.gameObject);

        if (PlayerController.instance != null)
		    Destroy(PlayerController.instance.gameObject);

		Destroy(GameMenu.instance.gameObject);

        Destroy(AudioManager.instance.gameObject);

        Destroy(BattleManager.instance.gameObject);

		SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadLastSave()
    {
		Destroy(GameManager.instance.gameObject);

		Destroy(PlayerController.instance.gameObject);

		Destroy(GameMenu.instance.gameObject);

		Destroy(BattleManager.instance.gameObject);

		SceneManager.LoadScene(loadGameScene);
	}
}
