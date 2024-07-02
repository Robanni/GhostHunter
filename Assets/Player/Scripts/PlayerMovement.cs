using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SimpleTouchController _touchController;
    [SerializeField] private PlayerLife _playerLife;
    
    
    private float _movementSpeed;
    private CharacterController _characterController;
    private Vector3 _moveDiraction;

    private void Start()
    {
        _movementSpeed = PlayerCharacteristics.Instance.GetSpeed();
        _characterController = GetComponent<CharacterController>();

        PlayerCharacteristics.Instance.onCharacteristicChanged += UpdataCharacteristic;
    }
    private void FixedUpdate()
    {
        if (_playerLife.IsDead()) return;
        MoveCharacter();
        RotateCharacter();
    }

    private void MoveCharacter()
    {
        if (_touchController == null) return;
        Vector3 moveDiraction = new Vector3(_touchController.GetTouchPosition.x, 0, _touchController.GetTouchPosition.y);

        _moveDiraction = moveDiraction;

        moveDiraction *= _movementSpeed;

        _characterController.Move(moveDiraction*Time.deltaTime);
    }

    private void RotateCharacter()
    {
        if(Vector3.Angle(transform.forward, _moveDiraction) > 0)
        {
            Vector3 rotateDiraction = Vector3.RotateTowards(transform.forward,_moveDiraction,1,0);
            transform.rotation = Quaternion.LookRotation(rotateDiraction);
        }
    }

    private void UpdataCharacteristic()
    {
        _movementSpeed = PlayerCharacteristics.Instance.GetSpeed();
    }
}
