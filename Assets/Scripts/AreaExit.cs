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

    // Start is called before the first frame update
    void Start()
    {
        if (theEntrance != null)
            theEntrance.TransitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
            SceneManager.LoadScene(areaToLoad);

            PlayerController.instance.AreaTransitionName = areaTransitionName;
		}
	}
}
