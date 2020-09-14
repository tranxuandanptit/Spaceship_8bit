using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;
    public float Horizontal => _horizontal;
    public float Vertical => _vertical;

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Gameplay)
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
        }
    }
}
