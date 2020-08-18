using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Mirror;

public class GameController : Singleton<GameController>
{
    [SerializeField] Text playerHand;
	[SerializeField] Text enemyHand;
    [SerializeField] Text NameLabel;
    [SerializeField] Text MoneyLabel;
    [SerializeField] PlayerPanelController Panel1;
    [SerializeField] PlayerPanelController Panel2;

    public string PlayerHand { get { return playerHand.text; } }
    public string EnemyHand { get { return enemyHand.text; } }
    public PlayerPanelController ControlPanel1 { get { return Panel1; } }
    public PlayerPanelController ControlPanel2 { get { return Panel2; } }

    public List<int> Results = new List<int>();

    private void Start()
    {
        NameLabel.text = "-";
        MoneyLabel.text = "$0";
        ResetEnemyHand();
        ResetPlayerHand();
    }

    public void SetNameLabel(string name)
    {
        NameLabel.text = name;
    }
    public void SetMoneyLabel(int newCoins)
    {
        MoneyLabel.text = "$" + newCoins;
    }
    public void SetPlayerHand(int item)
    {
        playerHand.text = GetItemName(item);
    }
    public void SetEnemyHand(int item)
    {
        enemyHand.text = GetItemName(item);
    }
    public void ResetPlayerHand()
    {
        playerHand.text = "?";
    }
    public void ResetEnemyHand()
    {
        enemyHand.text = "?";
    }

    public int CalculateReward()
    {
        return GetCoinsAmount((UseableItem)Results[1], (UseableItem)Results[0]);
    }
    private int GetCoinsAmount(UseableItem playerHand, UseableItem opponentHand)
    {
        Result drawResult = ResultAnalyzer.GetResultState(playerHand, opponentHand);

        if (drawResult.Equals(Result.Won))
        {
            return 10;
        }
        else if (drawResult.Equals(Result.Lost))
        {
            return -10;
        }
        else
        {
            return 0;
        }
    }
    private string GetItemName(int item)
    {
        switch (item)
        {
            case 1:
                return UseableItem.Rock.ToString();
            case 2:
                return UseableItem.Paper.ToString();
            case 3:
                return UseableItem.Scissors.ToString();
        }
        return "";
    }
    
}