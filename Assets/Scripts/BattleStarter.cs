using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    [SerializeField]
    private BattleType[] potentialBattles;

    [SerializeField]
    private bool activateOnEnter;

    [SerializeField]
    private bool activateOnStay;

    [SerializeField]
    private bool activateOnExit;

    [SerializeField]
    private bool deactivateAfterStarting;

    [SerializeField]
    private bool cannotFlee;

    public bool CannotFlee
    {
        get
        {
            return cannotFlee;
        }

        set
        {
            cannotFlee = value;
        }
    }

	public float battleProbability = 0.1f; // 10% chance for a battle after stepsThreshold

	public int stepsThreshold = 10;

	private int stepsSinceLastBattle = 0;

	private bool inArea;

    [SerializeField]
    private bool shouldCompleteQuest;

    [SerializeField]
    private string questToComplete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea && PlayerController.instance.CanMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
			{
				stepsSinceLastBattle++;

				if (stepsSinceLastBattle >= stepsThreshold)
				{
					TryStartBattle();
				}
			}
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
        {
            if (activateOnEnter)
            {
                TryStartBattle();
            }
            else
            {
                inArea = true;
            }
                
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player" && this.tag != "Boss")
		{
            if (activateOnExit)
            {
                TryStartBattle();
			}
            else
            {
				inArea = false;
			}
			
		}
        else if (collision.tag == "Player" && this.tag == "Boss")
        {
            StartCoroutine(StartBattleCo());
        }
	}

	private void TryStartBattle()
	{
		if (Random.value <= battleProbability)
		{
			StartCoroutine(StartBattleCo());
		}
		stepsSinceLastBattle = 0;
	}

	public IEnumerator StartBattleCo()
    {
        UIFade.instance.FadeToBlack();

        GameManager.instance.BattleActive = true;

        int selectedBattle = Random.Range(0, potentialBattles.Length);

        BattleManager.instance.RewardItems = potentialBattles[selectedBattle].rewardItems;

        BattleManager.instance.EnemyBaseXP = potentialBattles[selectedBattle].baseEnemyXp;

        yield return new WaitForSeconds(1.5f);

        BattleManager.instance.BattleStart(potentialBattles[selectedBattle].enemies, cannotFlee);

        UIFade.instance.FadeFromBlack();

        if (deactivateAfterStarting)
        {
            gameObject.SetActive(false);
        }

        BattleReward.instance.MarkQuestComplete = shouldCompleteQuest;

        BattleReward.instance.QuestToMark = questToComplete;
    }
}