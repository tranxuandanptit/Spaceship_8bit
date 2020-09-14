using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody2d;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _shotSpeed;
    [SerializeField]
    private float _shotRate;
    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private Transform _shotPoint;
    private Vector3 _screenSizeWolrd;
    public bool _havePower;
    [SerializeField]
    private GameObject _powerUpPrefab;
    [SerializeField]
    private PoolName _poolName;
    [SerializeField]
    private LayerMask _playerLayer;
    [SerializeField]
    private LayerMask _destroyLayer;
    [SerializeField]
    private LayerMask _projectileLayer;
    private void Start()
    {

        StartCoroutine(IeShot());
        _screenSizeWolrd = GameManager.Instance.MainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }
    private void OnEnable()
    {
        Init();
    }
    private void Init()
    {
        _havePower = false;
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Gameplay)
        {
            _rigidbody2d.velocity = transform.up * _speed * Time.deltaTime;
        }
        else
        {
            _rigidbody2d.velocity = Vector2.zero;
        }
    }
    private IEnumerator IeShot()
    {
        float rand;
        while (true)
        {
            if (GameManager.Instance.CurrentGameState == GameState.Gameplay)
            {

                yield return new WaitForSeconds(1 / _shotSpeed);
                if (transform.position.x > -_screenSizeWolrd.x && transform.position.x < _screenSizeWolrd.x && transform.position.y > -_screenSizeWolrd.y && transform.position.x < _screenSizeWolrd.y)
                {
                    rand = Random.Range(0.0f, 1.0f);
                    if (rand < _shotRate)
                    {
                        GameObject projectile = Pool.Instance.Instantiate(PoolName.ENEMY_PROJETILE, _projectile, _shotPoint.position, Quaternion.identity);
                        SoundManager.Instance.Play("Enemy_Shoot");
                        if (GameManager.Instance.CurrentGameState == GameState.Gameplay)
                            projectile.transform.up = PlayerMovement.Instance.transform.position - transform.position;
                    }
                }
            }
            else
            {
                yield return null;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (0 != (_playerLayer.value & 1 << other.gameObject.layer))
        {
            if (_havePower)
            {
                Pool.Instance.Instantiate(PoolName.POWER_UP, _powerUpPrefab, transform.position, Quaternion.identity);
            }
            GameManager.Instance.Score += _poolName == PoolName.ENEMY_1 ? 10 : _poolName == PoolName.ENEMY_2 ? 100 : 200;
            GameManager.Instance.Explosion(transform.position);
            Pool.Instance.Destroy(_poolName, this.gameObject);

        }
        if (0 != (_projectileLayer.value & 1 << other.gameObject.layer))
        {
            if (_havePower)
            {
                Pool.Instance.Instantiate(PoolName.POWER_UP, _powerUpPrefab, transform.position, Quaternion.identity);
            }
            GameManager.Instance.Score += _poolName == PoolName.ENEMY_1 ? 10 : _poolName == PoolName.ENEMY_2 ? 100 : 200;
            GameManager.Instance.Explosion(transform.position);
            Pool.Instance.Destroy(_poolName, this.gameObject);

        }
        if (0 != (_destroyLayer.value & 1 << other.gameObject.layer))
        {
            Pool.Instance.Destroy(_poolName, this.gameObject);
        }
    }
}
