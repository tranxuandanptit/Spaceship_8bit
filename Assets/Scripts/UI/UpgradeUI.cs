using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : UIController
{
    [SerializeField]
    private Text _coinText;
    protected override void Start()
    {
        base.Start();
        EventHub.Instance.RegisterEvent(EventName.UPDATE_COIN, ChangeCoinValue);
        _coinText.text = StorageUserInfo.Instance.Coin.ToString();
    }
    public void OnQuitButtonClicked()
    {
        GameManager.Instance.CurrentGameState = GameState.GameMenu;
    }
    public void ChangeCoinValue(object data)
    {
        _coinText.text = data.ToString();
    }
}
