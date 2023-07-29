using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChar : MonoBehaviour
{
    [SerializeField]
    private bool isPlayer;

    [SerializeField]
    private string[] movesAvailable;

    [SerializeField]
    private bool hasDied;

    [SerializeField]
    private string charName;

    [SerializeField]
    private int currentXp;

    [SerializeField]
    private int currentHP;

    [SerializeField]
    private int maxHp;

    [SerializeField]
    private int currentMp;

    [SerializeField]
    private int maxMp;

    [SerializeField]
    private int strength;

    [SerializeField]
    private int defence;

    [SerializeField]
    private int weaponPower;

    [SerializeField]
    private int armorPower;
}
