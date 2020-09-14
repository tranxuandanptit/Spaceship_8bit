using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : UIController
{
    [SerializeField]
    private Sprite _muteSprite;
    [SerializeField]
    private Sprite _unmuteSprite;
    [SerializeField]
    private Image _soundButtonRenderer;

    protected override void Start()
    {
        base.Start();
        EventHub.Instance.RegisterEvent(EventName.UPDATE_SOUND_SETTING, ChangeSoundSetting);
        _soundButtonRenderer.sprite = StorageUserInfo.Instance.IsMute ? _muteSprite : _unmuteSprite;
    }

    public void OnPlayButtonClicked()
    {
        GameManager.Instance.CurrentGameState = GameState.Gameplay;
        GameManager.Instance.PlayerSpawn();
    }
    public void OnHighscoreButtonClicked()
    {
        GameManager.Instance.CurrentGameState = GameState.Highscore;
    }
    public void OnUpgradeButtonClicked()
    {
        GameManager.Instance.CurrentGameState = GameState.Upgrade;
    }
    public void OnCreditButtonClicked()
    {
        GameManager.Instance.CurrentGameState = GameState.Credit;
    }
    public void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void OnSoundButtonClicked()
    {
        StorageUserInfo.Instance.IsMute = !StorageUserInfo.Instance.IsMute;
    }
    private void ChangeSoundSetting(object data)
    {
        bool isMute = (bool)data;
        _soundButtonRenderer.sprite = isMute ? _muteSprite : _unmuteSprite;
    }
}
