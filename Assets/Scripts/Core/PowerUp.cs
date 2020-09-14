using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private LayerMask _playerLayer;
    [SerializeField]
    private PoolName _poolName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (0 != (_playerLayer.value & 1 << other.gameObject.layer))
        {
            GameManager.Instance.CurrentPowerUp += 1;
            SoundManager.Instance.Play("PowerUp");
            Pool.Instance.Destroy(_poolName, this.gameObject);
        }
    }
}
