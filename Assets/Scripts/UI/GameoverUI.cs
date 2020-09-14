using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverUI : UIController
{
    [SerializeField]
    private Text _coinText;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _bonusCoinText;

    protected override void Start()
    {
        base.Start();
        EventHub.Instance.RegisterEvent(EventName.UPDATE_SCORE, ChangeScore);
        _scoreText.text = "Score" + GameManager.Instance.Score;
    }
    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("Game");
    }
    protected override void ChangeGameState(object data)
    {
        base.ChangeGameState(data);
        if (_currentGameState == _gameState)
        {
            _coinText.text = StorageUserInfo.Instance.Coin.ToString();
        }
    }
    private void ChangeScore(object data)
    {
        _scoreText.text = GameManager.Instance.GetHighScore ? "Highscore: " : "Score: " + data.ToString();
        if (GameManager.Instance.CurrentGameState == GameState.Gameover)
        {
            _bonusCoinText.text = (Mathf.RoundToInt(GameManager.Instance.Score / 100)).ToString();
            Debug.Log(Mathf.RoundToInt(GameManager.Instance.Score / 100));
            StorageUserInfo.Instance.Coin += Mathf.RoundToInt(GameManager.Instance.Score / 100);
        }
    }
}
