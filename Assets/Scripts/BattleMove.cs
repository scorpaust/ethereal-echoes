using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleMove 
{
    [SerializeField]
    private string moveName;

    public string MoveName
    {
        get
        {
            return moveName;
        }

        private set { }
    }

    [SerializeField]
    private int movePower;

    public int MovePower
    {
        get
        {
            return movePower;
        }

        private set { }
    }

    [SerializeField]
    private int moveCost;

    [SerializeField]
    private AttackEffect theEffect;

    public AttackEffect TheEffect
    {
        get
        {
            return theEffect;
        }

        private set { }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
