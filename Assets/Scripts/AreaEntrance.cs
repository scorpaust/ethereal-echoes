using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField]
    private string transitionName;

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
        if (PlayerController.instance != null && transitionName == PlayerController.instance.AreaTransitionName)
		{
            PlayerController.instance.transform.position = transform.position;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
