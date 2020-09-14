using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class StorageUserInfo
{
    #region BASE
    public static StorageUserInfo _Instance;
    public static StorageUserInfo Instance
    {
        get
        {
            if (_Instance == null)
            {
                StorageUserInfo ouput = new StorageUserInfo();
                ouput._Init(ouput);
            }
            return _Instance;
        }
    }
    protected string FileName => Define.FN_USERDATA;
    private string getFileName => string.Format($"{FileName}.data");
    private string FilePath => Path.Combine(Application.persistentDataPath, this.getFileName);
    private void _Init(StorageUserInfo instance)
    {
        _Instance = SaveAndLoadData.Deserialize(FilePath) == null ? instance : (StorageUserInfo)SaveAndLoadData.Deserialize(FilePath);
    }
    private void Serialize()
    {
        SaveAndLoadData.Serialize(Instance, FilePath);
    }
    public void Reset()
    {
        File.Delete(FilePath);
    }
    public void Load() { }

    public StorageUserInfo()
    {
        _highscores = new List<int>();
        _coin = 100;
        _isMute = false;
    }
    #endregion

    private List<int> _highscores;
    public List<int> Hightscores
    {
        get
        {
            return _highscores;
        }
    }

    private int _coin;
    public int Coin
    {
        get
        {
            return _coin;
        }
        set
        {
            _coin = value;
            EventHub.Instance.UpdateEvent(EventName.UPDATE_COIN, _coin);
            Serialize();
        }
    }

    private bool _isMute;
    public bool IsMute
    {
        get
        {
            return _isMute;
        }
        set
        {
            _isMute = value;
            EventHub.Instance.UpdateEvent(EventName.UPDATE_SOUND_SETTING, _isMute);
            Serialize();
        }
    }

    public bool CheckAndSaveHighScore(int score)
    {
        if (_highscores.Count > 0)
        {
            List<int> listLess = _highscores.FindAll(value => value <= score);
            if (listLess.Count > 0)
            {
                _highscores.Insert(_highscores.IndexOf(_highscores.FindAll(value => value <= score)[0]), score);
                if (_highscores.Count > 10)
                {
                    _highscores.RemoveAt(_highscores.Count - 1);
                }
                EventHub.Instance.UpdateEvent(EventName.UPDATE_HIGHSCORE, _highscores);
                Serialize();
                return true;
            }
        }
        else
        {
            _highscores.Add(score);
            EventHub.Instance.UpdateEvent(EventName.UPDATE_HIGHSCORE, _highscores);
            Serialize();
            return true;
        }
        return false;
    }
}



