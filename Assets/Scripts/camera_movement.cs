using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour
{
    [SerializeField] private Transform _player;

    void LateUpdate()
    {
        Vector3 _temp = transform.position;
        _temp.x = _player.position.x;
        _temp.y = _player.position.y;

        transform.position = _temp;
    }
}
