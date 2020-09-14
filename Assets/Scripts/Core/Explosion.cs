using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private Animator _Animator;

    void OnEnable()
    {
        StartCoroutine(IeWaitToDestroy());
    }

    private IEnumerator IeWaitToDestroy()
    {
        yield return new WaitForSeconds(_Animator.GetCurrentAnimatorClipInfo(0).Length - 0.1f);
        Pool.Instance.Destroy(PoolName.EXPLOSION, this.gameObject);
    }
}
