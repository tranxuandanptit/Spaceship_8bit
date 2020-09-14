using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private float _shotSpeed;
    public float ShotSpeed
    {
        get
        {
            return _shotSpeed + _currentPowerUp * 0.4f;
        }
    }
    [SerializeField]
    private Camera _mainCam;
    public Camera MainCam => _mainCam;
    [SerializeField]
    private float _spawnerRate;
    [SerializeField]
    private List<EnemySpawner> _enemySpawners;
    private int _currentPowerUp;
    public int CurrentPowerUp
    {
        get
        {
            return _currentPowerUp;
        }
        set
        {
            if (_currentPowerUp <= 6)
            {
                _currentPowerUp = value;
                Debug.Log("Power Up");
            }
        }
    }
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private Transform _playerSpawnPoint;
    private int _currentSpawnerIndex = 0;
    private GameState _currentGameState;
    public GameState CurrentGameState
    {
        get
        {
            return _currentGameState;
        }
        set
        {
            _currentGameState = value;
            Debug.Log(_currentGameState);
            if (_currentGameState == GameState.Gameover)
            {
                _getHighscore = StorageUserInfo.Instance.CheckAndSaveHighScore(_score);
            }
            EventHub.Instance.UpdateEvent(EventName.CHANGE_GAMESTATE, _currentGameState);
        }
    }
    [SerializeField]
    private GameObject _explosionPrefab;
    private int _score;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            EventHub.Instance.UpdateEvent(EventName.UPDATE_SCORE, _score);
        }
    }
    private bool _getHighscore;
    public bool GetHighScore => _getHighscore;
    [SerializeField]
    private int _maxHealth;
    public int MaxHealth => _maxHealth;
    private float _health;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            EventHub.Instance.UpdateEvent(EventName.UPDATE_HEALTH, _health);
        }
    }
    public bool GameOver;
    protected override void Awake()
    {
        base.Awake();

    }
    private void Start()
    {
        Init();
        StartCoroutine(IeSpawnEnemy());

    }
    private void Init()
    {
        StorageUserInfo.Instance.Load();
        _currentGameState = GameState.GameMenu;
        _currentPowerUp = 1;
        _health = _maxHealth;
        GameOver = false;
        SoundManager.Instance.Play("BGM");
    }
    public void PlayerSpawn()
    {
        Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
    }
    private static void ResetData()
    {
        StorageUserInfo.Instance.Reset();
    }
    private IEnumerator IeSpawnEnemy()
    {
        int rand = _currentSpawnerIndex;
        while (true)
        {
            if (_currentGameState == GameState.Gameplay)
            {
                yield return new WaitForSeconds(_spawnerRate);
                while (rand == _currentSpawnerIndex)
                {
                    rand = Random.Range(0, _enemySpawners.Count - 1);
                }
                _enemySpawners[rand].Spawner();
                _currentSpawnerIndex = rand;
            }
            else
            {
                yield return null;
            }
        }
    }
    public void Explosion(Vector3 position)
    {
        Pool.Instance.Instantiate(PoolName.EXPLOSION, _explosionPrefab, position, Quaternion.identity);
        SoundManager.Instance.Play("Explosion");
    }
}
