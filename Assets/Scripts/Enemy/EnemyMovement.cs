using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private Animator _animator;

    public int damage = 1;

    // Kryptys animacijoms
    private enum MovementDirection { Down, Up, Left, Right }
    private MovementDirection _currentDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        MoveTowardsTarget();
        UpdateAnimationDirection();
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        else
            _targetDirection = Vector2.zero;
    }

    private void MoveTowardsTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            return;
        }
        _rigidbody.linearVelocity = _targetDirection.normalized * _speed;
    }

    private void UpdateAnimationDirection()
    {
        if (_targetDirection == Vector2.zero)
        {
            //_animator.SetBool("IsMoving", false);
            return;
        }

        //_animator.SetBool("IsMoving", true);

        // Nustatome pagrindinę kryptį
        if (Mathf.Abs(_targetDirection.x) > Mathf.Abs(_targetDirection.y))
        {
            // Horizontalus judėjimas (kairė/dešinė) turi pirmenybę
            _currentDirection = _targetDirection.x > 0 ? MovementDirection.Right : MovementDirection.Left;
        }
        else
        {
            // Vertikalus judėjimas (viršus/apačia)
            _currentDirection = _targetDirection.y > 0 ? MovementDirection.Up : MovementDirection.Down;
        }

        // Nustatome animatoriaus parametrus
        //_animator.SetInteger("Direction", (int)_currentDirection);
    }
}

//using UnityEngine;

//public class EnemyMovement : MonoBehaviour
//{
//    [SerializeField]
//    private float _speed;

//    [SerializeField]
//    private float _rotationSpeed;

//    private Rigidbody2D _rigidbody;
//    private PlayerAwarenessController _playerAwarenessController;
//    private Vector2 _targetDirection;

//    private void Awake()
//    {
//        _rigidbody = GetComponent<Rigidbody2D>();
//        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
//    }

//    private void FixedUpdate()
//    {
//        UpdateTargetDirection();
//        MoveTowardsTarget();
//    }

//    private void UpdateTargetDirection()
//    {
//        if (_playerAwarenessController.AwareOfPlayer)
//        {
//            _targetDirection = _playerAwarenessController.DirectionToPlayer;
//        }
//        else
//        {
//            _targetDirection = Vector2.zero;
//        }
//    }

//    private void MoveTowardsTarget()
//    {
//        if (_targetDirection == Vector2.zero)
//        {
//            _rigidbody.linearVelocity = Vector2.zero;
//            return;
//        }

//        // Tiesioginis judėjimas į taikinį (be sukimosi)
//        _rigidbody.linearVelocity = _targetDirection.normalized * _speed;
//    }
//}

//----------------------------------------------------------------------------------------------------------------------------------------------------

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyMovement : MonoBehaviour
//{
//    [SerializeField]
//        private float _speed;

//    [SerializeField]
//        private float _rotationSpeed;

//    private Rigidbody2D _rigidbody;
//    private PlayerAwarenessController _playerAwarenessController;
//    private Vector2 _targetDirection;

//    private void Awake()
//    {
//        _rigidbody = GetComponent<Rigidbody2D>();
//        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
//    }

//    private void FixedUpdate()
//    {
//        UpdateTargetDirection();
//        RotateTowardsTarget();
//        SetVelocity();
//    }

//    private void UpdateTargetDirection()
//    {
//        if (_playerAwarenessController.AwareOfPlayer)
//        {
//            _targetDirection = _playerAwarenessController.DirectionToPlayer;
//        }
//        else
//        {
//            _targetDirection = Vector2.zero;
//        }
//    }

//    private void RotateTowardsTarget()
//    {
//        if (_targetDirection == Vector2.zero)
//        {
//            return;
//        }

//        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
//        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

//        _rigidbody.SetRotation(rotation);
//    }

//    private void SetVelocity()
//    {
//        if (_targetDirection == Vector2.zero)
//        {
//            _rigidbody.linearVelocity = Vector2.zero;
//        }
//        else
//        {
//            _rigidbody.linearVelocity = transform.up * _speed;
//        }
//    }
//}