using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField]
    private string transitionName;

    private bool alreadyPositioned = false;

    public string TransitionName
	{
        get
		{
            return transitionName;
		}

        set
		{
            transitionName = value;
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        UIFade.instance.FadeFromBlack();

        if (GameManager.instance)
            GameManager.instance.FadingBetweenAreas = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (alreadyPositioned) return;

		if (PlayerController.instance != null && transitionName == PlayerController.instance.AreaTransitionName)
		{
			PlayerController.instance.transform.position = transform.position;

            alreadyPositioned = true;
		}

	}
}
