using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Rigidbody2D _rigidbody2d;
    [SerializeField]
    private LayerMask _destroyLayer;
    [SerializeField]
    private LayerMask _playerLayer;
    [SerializeField]
    private PoolName _poolName;
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
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (0 != (_destroyLayer.value & 1 << other.gameObject.layer))
        {
            Pool.Instance.Destroy(_poolName, this.gameObject);
        }
    }
}
