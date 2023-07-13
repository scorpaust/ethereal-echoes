using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private Tilemap theMap;

    private Vector3 bottomLeftLimit, topRightLimit;

    private float halfHeight, halfWidth;

    [SerializeField]
    private int musicToPlay;

    private bool musicStarted;

    // Start is called before the first frame update
    void Start()
    {
        halfHeight = Camera.main.orthographicSize;

        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);

        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

        if (PlayerController.instance != null)
            PlayerController.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

	private void Update()
	{
		if (PlayerController.instance != null)
        {
			target = FindObjectOfType<PlayerController>().transform;
		}
		else
        {
            return;
        }

	}

	// LateUpdate is called once per frame after update
	void LateUpdate()
    {
        if (target != null)
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Keep the camera inside the bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    
        if (!musicStarted)
        {
            musicStarted = true;

            AudioManager.instance.PlayBgm(musicToPlay);
        }
    }
}
