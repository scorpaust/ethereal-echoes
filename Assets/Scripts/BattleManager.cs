using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [SerializeField]
    private GameObject battleScene;

    [SerializeField]
    private Transform[] playerPositions;

    [SerializeField]
    private Transform[] enemyPositions;

    [SerializeField]
    private BattleChar[] playerPrefabs;

    [SerializeField]
    private BattleChar[] enemyPrefabs;

    private bool battleActive;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            BattleStart(new string[] { "Eyeball", "Skeleton", "Orc" });
        }
    }

    public void BattleStart(string[] enemiesToSpawn)
    {
        if (!battleActive)
        {
            battleActive = true;

            GameManager.instance.BattleActive = true;

            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);

            battleScene.SetActive(true);

            AudioManager.instance.PlayBgm(0);
        }
    }
}
