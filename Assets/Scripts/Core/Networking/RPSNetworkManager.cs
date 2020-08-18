using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RPSNetworkManager : NetworkManager
{
    List<NetworkPlayer> _players;
    public int ActivePlayerIndex { get; private set; }
    public static RPSNetworkManager Instance { get; private set; }

    public override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    public override void Start()
    {
        base.Start();
        _players = new List<NetworkPlayer>();
    }

    public void SubscribePlayer(NetworkPlayer player)
    {
        player.GetComponent<NetworkPlayer>().AssignPanel(
            numPlayers == 0 ? GameController.Instance.ControlPanel1 
            : GameController.Instance.ControlPanel2);

        if (_players.Count <= 2)
            _players.Add(player);

        if (_players.Count == 2)
        {
            if (AreAllPlayersReady())
                StartActivePlayer();
        }
    }
    public void UnsubscribePlayer(NetworkPlayer player)
    {
        if (_players.Count > 0)
            _players.Remove(player);
    }
    public void SwitchTurns()
    {
        _players[ActivePlayerIndex].EndTurn();
        ActivePlayerIndex = (ActivePlayerIndex + 1) % _players.Count;
        _players[ActivePlayerIndex].StartTurn();
    }

    private bool AreAllPlayersReady()
    {
        return _players.All(p => p.IsReady);
    }
    private void StartActivePlayer()
    {
        _players[ActivePlayerIndex].StartGame();
    }
}
