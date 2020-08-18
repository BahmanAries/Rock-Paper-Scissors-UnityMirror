using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void PlayerCallBack(int item, int coins);
    public event PlayerCallBack OnTurnEnd;
    bool isLocalPlayer = false;

    public int ID { get; private set; }
    public int Coins { get; private set; }
    public string Name { get; private set; }

    public PlayerPanelController ControlPanel { get; private set; }

    public void Initialize(int id, string name)
    {
        ID = id;
        Name = name;
        isLocalPlayer = true;
    }
    public void AssignControlPanel(PlayerPanelController panel)
    {
        ControlPanel = panel;
        ControlPanel.OnButtonClicked += ControlPanel_OnButtonClicked;
    }
    public void AddCoins(int coins)
    {
        Coins += coins;
    }
    private void ControlPanel_OnButtonClicked(int item)
    {
        if (!isLocalPlayer)
            return;

        EndTurn(item);
    }

    public void StartTurn()
    {
        if (isLocalPlayer)
        {
            ControlPanel.Enable();
        }
    }

    public void EndTurn(int myResult)
    {
        if (isLocalPlayer)
        {
            ControlPanel.Disable();
            GameController.Instance.SetPlayerHand(myResult);
            OnTurnEnd(myResult, Coins);
        }
    }


}
