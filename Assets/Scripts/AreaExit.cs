using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField]
    private string areaToLoad;

    [SerializeField]
    private string areaTransitionName;

    [SerializeField]
    private AreaEntrance theEntrance;

    private float waitToLoad = 1f;

    private bool shouldLoadAfterFade;

    // Start is called before the first frame update
    void Start()
    {
        if (theEntrance != null)
            theEntrance.TransitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
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
		if (other.CompareTag("Player"))
		{
            shouldLoadAfterFade = true;

            UIFade.instance.FadeToBlack();

            PlayerController.instance.AreaTransitionName = areaTransitionName;
		}
	}
}
