using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [SerializeField]
    private Rigidbody2D _rigidbody2d;
    [SerializeField]
    private PlayerInput _input;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Transform _middleShotPoint;
    [SerializeField]
    private Transform _leftShotPoint;
    [SerializeField]
    private Transform _rightShotPoint;
    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private LayerMask _damageLayer;

    private void Start()
    {
        StartCoroutine(IeShot());
    }
    private void FixedUpdate()
    {
        _rigidbody2d.velocity = new Vector2(_input.Horizontal, _input.Vertical) * _speed * Time.deltaTime;
    }
    private IEnumerator IeShot()
    {
        while (true)
        {
            if (GameManager.Instance.CurrentGameState == GameState.Gameplay)
            {
                Pool.Instance.Instantiate(PoolName.PLAYER_PROJETILE, _projectile, _middleShotPoint.position, Quaternion.identity);
                SoundManager.Instance.Play("Player_Shoot");
                yield return new WaitForSeconds(1 / GameManager.Instance.ShotSpeed);
            }
            else
                yield return null;
        }

    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (0 != (_damageLayer.value & 1 << other.gameObject.layer))
        {
            GameManager.Instance.Health -= 1;
            if (GameManager.Instance.Health <= 0)
            {
                GameManager.Instance.CurrentGameState = GameState.Gameover;
                GameManager.Instance.Explosion(transform.position);
                Destroy(this.gameObject);
            }
        }
    }
    // private void ChangeGameState(object data)
    // {
    //     GameState current = (GameState)data;
    //     if (_currentGameState !)
    //     {
    //         _root.SetActive(true);
    //     }
    //     else
    //     {
    //         _root.SetActive(false);
    //     }
    // }
}
