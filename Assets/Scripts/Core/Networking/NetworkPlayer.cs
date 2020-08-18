using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    public bool IsReady;

    [SyncVar]
    public bool IsMyTurn;

    [SerializeField]
    PlayerController controller;

    public override void OnStartClient()
    {
        base.OnStartClient();
        MakeReady();
        RPSNetworkManager.Instance.SubscribePlayer(this);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        controller.Initialize((int)netId, "Player"+netId);
        GameController.Instance.SetNameLabel(controller.Name);
    }
    public void AssignPanel(PlayerPanelController panel)
    {
        controller.AssignControlPanel(panel);
    }

    [Server]
    public void MakeReady()
    {
        IsReady = true;
    }

    void Start()
    {
        controller.OnTurnEnd += CmdOnPlayerTurnEnded;
    }

    public void StartGame()
    {
        StartTurn();
    }

    [Server]
    public void StartTurn()
    {
        IsMyTurn = true;
        RpcStartTurn();
    }

    [ClientRpc]
    void RpcStartTurn()
    {
        controller.StartTurn();
    }

    [Server]
    public void EndTurn()
    {
        IsMyTurn = false;
    }

    [Command]
    void CmdOnPlayerTurnEnded(int item, int coins)
    {
        RpcOnPlayerTurnEnded(item, coins);
        RPSNetworkManager.Instance.SwitchTurns();
    }
    [ClientRpc]
    void RpcOnPlayerTurnEnded(int item, int coins)
    {
        GameController.Instance.Results.Add(item);

        if (GameController.Instance.Results.Count == 2)
        {
            if (hasAuthority)
            {
                GameController.Instance.SetPlayerHand(item);
                GameController.Instance.SetEnemyHand(GameController.Instance.Results[0]);
                controller.AddCoins(GameController.Instance.CalculateReward());
                GameController.Instance.SetMoneyLabel(controller.Coins);
            }
            else
            {
                GameController.Instance.SetEnemyHand(item);
                GameController.Instance.SetPlayerHand(GameController.Instance.Results[0]);
                controller.AddCoins(-1 * GameController.Instance.CalculateReward());
                GameController.Instance.SetMoneyLabel(controller.Coins);
            }

            GameController.Instance.Results.Clear();
        }
        else
        {
            if (hasAuthority)
                GameController.Instance.ResetEnemyHand();
            else
            {
                GameController.Instance.ResetPlayerHand();
                GameController.Instance.ResetEnemyHand();
            }
        }
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        RPSNetworkManager.Instance.UnsubscribePlayer(this);
    }

}
