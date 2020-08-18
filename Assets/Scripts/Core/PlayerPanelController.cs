using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelController : MonoBehaviour
{
    [SerializeField]
    Button BtnRock;
    [SerializeField]
    Button BtnScissor;
    [SerializeField]
    Button BtnPaper;


    public delegate void GUICallBack(int item);
    public event GUICallBack OnButtonClicked;

    void Start()
    {
        Disable();
    }

    public void Enable()
    {
        BtnPaper.interactable = BtnRock.interactable = BtnScissor.interactable = true;
    }
    public void Disable()
    {
        BtnPaper.interactable = BtnRock.interactable = BtnScissor.interactable = false;
    }
    public void BtnClick(int item)
    {
        OnButtonClicked(item);
    }

}
