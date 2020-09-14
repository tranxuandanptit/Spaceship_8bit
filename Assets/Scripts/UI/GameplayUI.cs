using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : UIController
{
    [SerializeField]
    private Text _coinText;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Slider _healthBar;
    protected override void Start()
    {
        base.Start();
        EventHub.Instance.RegisterEvent(EventName.UPDATE_COIN, ChangeCoinValue);
        _coinText.text = StorageUserInfo.Instance.Coin.ToString();
        EventHub.Instance.RegisterEvent(EventName.UPDATE_SCORE, ChangeScore);
        _scoreText.text = "Score: " + GameManager.Instance.Score;
        EventHub.Instance.RegisterEvent(EventName.UPDATE_HEALTH, ChangeHealth);
        _healthBar.value = (float)(GameManager.Instance.Health / GameManager.Instance.MaxHealth);
    }
    public void OnPauseButtonClicked()
    {
        GameManager.Instance.CurrentGameState = GameState.Pause;
    }
    public void ChangeCoinValue(object data)
    {
        _coinText.text = data.ToString();
    }
    public void ChangeScore(object data)
    {
        _scoreText.text = "Score: " + data.ToString();
    }
    public void ChangeHealth(object data)
    {
        float input = (float)data;
        _healthBar.value = input / GameManager.Instance.MaxHealth;
    }
}
