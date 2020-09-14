using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreUI : UIController
{
    [SerializeField]
    private Text _highscoresContent;
    protected override void Start()
    {
        base.Start();
        EventHub.Instance.RegisterEvent(EventName.UPDATE_HIGHSCORE, ListenChangeHighscore);
        PrintHighscore(StorageUserInfo.Instance.Hightscores);
    }
    public void OnQuitButtonClicked()
    {
        GameManager.Instance.CurrentGameState = GameState.GameMenu;
    }

    private void ListenChangeHighscore(object data)
    {
        List<int> content = (List<int>)data;
        PrintHighscore(content);
    }
    private void PrintHighscore(List<int> highscores)
    {
        _highscoresContent.text = "";
        for (int i = 0; i < highscores.Count; i++)
        {
            _highscoresContent.text += $"#{i + 1}: {highscores[i]}";
            if (i < highscores.Count - 1)
            {
                _highscoresContent.text += "\n";
            }
        }
    }
    // int i = 30;
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.P))
    //     {
    //         StorageUserInfo.Instance.CheckAndSaveHighScore(i);
    //     }
    // }
}
