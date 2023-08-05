using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTargetButton : MonoBehaviour
{
    [SerializeField]
    private string moveName;

    public string MoveName
    {
        get
        {
            return moveName;
        }

        set
        {
            moveName = value;
        }
    }

    [SerializeField]
    private int activeBattlerTarget;

    public int ActiveBattlerTarget
    {
        get
        {
            return activeBattlerTarget;
        }

        set
        {
            activeBattlerTarget = value;
        }
    }

    [SerializeField]
    private Text targetName;

    public Text TargetName
    {
        get
        {
            return targetName;
        }

        set
        {
            targetName = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        BattleManager.instance.PlayerAttack(moveName, activeBattlerTarget);
    }
}
