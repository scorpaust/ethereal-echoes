using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleReward : MonoBehaviour
{
    public static BattleReward instance;

    [SerializeField]
    private Text xpText;

    [SerializeField]
    private Text itemsText;

    [SerializeField]
    private GameObject rewardsScreen;

    [SerializeField]
    private string[] rewardItems;

    [SerializeField]
    private int xpEarned;

    [SerializeField]
    private bool markQuestComplete;

    public bool MarkQuestComplete
    {
        get
        {
            return markQuestComplete;
        }

        set
        {
            markQuestComplete = value;
        }
    }

    [SerializeField]
    private string questToMark;

    public string QuestToMark
    {
        get
        {
            return questToMark;
        }

        set
        {
            questToMark = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenRewardsScreen(int xp, string[] items)
    {
        xpEarned = xp;

        rewardItems = items;

        xpText.text = "Everyone earned " + xpEarned.ToString() + " XP";

        itemsText.text = "";
        
        for (int i = 0; i < rewardItems.Length; i++)
        {
            itemsText.text += "\n" + rewardItems[i] + "\n";
        }
        
        rewardsScreen.SetActive(true);
    }

    public void CloseRewardsScreen()
    {
        for (int i = 0; i < GameManager.instance.PlayerStats.Length; i++)
        {
            if (GameManager.instance.PlayerStats[i].gameObject.activeInHierarchy)
            {
                GameManager.instance.PlayerStats[i].AddXp(xpEarned);
            }
        }

        for (int i = 0; i < rewardItems.Length; i++)
        {
            GameManager.instance.AddItem(rewardItems[i]);
        }

        rewardsScreen.SetActive(false);

        GameManager.instance.BattleActive = false;

        if (markQuestComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMark);
        }
	}
}
