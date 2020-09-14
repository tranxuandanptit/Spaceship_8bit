using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    protected GameObject _root;
    [SerializeField]
    protected GameState _gameState;
    protected GameState _currentGameState;

    protected virtual void Start()
    {
        EventHub.Instance.RegisterEvent(EventName.CHANGE_GAMESTATE, ChangeGameState);
        _root.SetActive(GameManager.Instance.CurrentGameState == _gameState ? true : false);
    }

    protected virtual void ChangeGameState(object data)
    {
        _currentGameState = (GameState)data;
        if (_currentGameState == _gameState)
        {
            _root.SetActive(true);
        }
        else
        {
            _root.SetActive(false);
        }
    }
}

public enum GameState
{
    GameMenu,
    Credit,
    Upgrade,
    Highscore,
    Pause,
    Gameplay,
    Gameover,

}
