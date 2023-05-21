using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator anim;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private string areaTransitionName;

    public string AreaTransitionName
    {
        get
		{
            return areaTransitionName;
		}

        set
		{
            areaTransitionName = value;
		}
    }

    public static PlayerController instance;

    private Vector3 bottomLeftLimit, topRightLimit;

    private bool canMove = true;

    public bool CanMove
	{
        get
		{
            return canMove;
		}
        set
		{
            canMove = value;
		}
	}
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        if (instance == null)
		{
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
		{
            if (instance != this)
                Destroy(gameObject);
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
		{
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        }
        else
		{
            rb.velocity = Vector2.zero;
		}

        anim.SetFloat("moveX", rb.velocity.x);

        anim.SetFloat("moveY", rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
		{
            if (canMove)
			{
                anim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));

                anim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        // Keep the player inside the bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    public void SetBounds(Vector3 bottomLeft, Vector3 topRight)
	{
        bottomLeftLimit = bottomLeft + new Vector3(1f, 1f, 0f);

        topRightLimit = topRight + new Vector3(-1f, -1f, 0f);
	}
}
