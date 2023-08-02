using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    [SerializeField]
    private Text damageText;

    [SerializeField]
    private float lifetime = 1f;

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private float placementJitter = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);

        transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
    }

    public void SetDamage(int damageAmount)
    {
        damageText.text = damageAmount.ToString();

        transform.position += new Vector3(Random.Range(-placementJitter, placementJitter), Random.Range(-placementJitter, placementJitter), 0f);
    }
}
