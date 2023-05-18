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
            Destroy(gameObject);
		}
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

        anim.SetFloat("moveX", rb.velocity.x);

        anim.SetFloat("moveY", rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
		{
            anim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));

            anim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }   
    }
}
