using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _goal;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private PoolName _poolName;
    [SerializeField]
    private float _spawnRate;
    [SerializeField]
    private int _numberEnemy;
    private bool _givePowerUp;
    [SerializeField]
    private float _powerUpRate;
    public void Spawner()
    {
        StartCoroutine(IeSpawner());
    }

    private IEnumerator IeSpawner()
    {
        _givePowerUp = false;
        int numberEnemy = _numberEnemy;
        float randGivePower;
        while (numberEnemy > 0)
        {
            yield return new WaitForSeconds(_spawnRate);
            GameObject enemy = Pool.Instance.Instantiate(_poolName, _enemyPrefab, transform.position, Quaternion.identity);
            enemy.transform.up = _goal.position - transform.position;
            numberEnemy--;
            if (!_givePowerUp)
            {
                randGivePower = Random.Range(0f, 1f);
                if (randGivePower < _powerUpRate)
                {
                    enemy.GetComponent<EnemyMovement>()._havePower = true;
                }
                _givePowerUp = true;
            }
        }
    }
}
