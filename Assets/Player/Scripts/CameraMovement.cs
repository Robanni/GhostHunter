using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _distanceBackward;
    [Range(1, 10)]
    [SerializeField] private float _scaleFactor;

    void Update()
    {
        if (_player.Equals(null)) return;
        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y + _scaleFactor, _player.position.z - _distanceBackward);
    }
}
