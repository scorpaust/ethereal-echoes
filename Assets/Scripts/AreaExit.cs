using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;
    public string areaTo;
    public string areaFrom;
 
    public AreaEntrance entrance;
 
    private float waitToLoad = 1f;

    private bool shouldLoadAfterFade;

    void Start() {
        entrance.TransitionName = areaFrom;
    }

    private void Update() {
        if (shouldLoadAfterFade)
		{
            waitToLoad -= Time.deltaTime;

            if (waitToLoad <= 0f)
			{
                shouldLoadAfterFade = false;

                SceneManager.LoadScene(areaToLoad);
			}
		}
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shouldLoadAfterFade = true;

            GameManager.instance.FadingBetweenAreas = true;

            UIFade.instance.FadeToBlack();

            PlayerController.instance.AreaTransitionName = areaTo;
        }
    }
}
