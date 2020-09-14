using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : UIController
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
    public void OnResumeButtonClicked()
    {
        GameManager.Instance.CurrentGameState = GameState.Gameplay;
    }
    public void OnMainMenuButtonClicked()
    {
        GameManager.Instance.GameOver = true;
        SceneManager.LoadScene("Game");
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
