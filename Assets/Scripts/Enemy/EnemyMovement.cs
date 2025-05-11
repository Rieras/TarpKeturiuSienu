using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private Animator _animator;

    public int damage = 1;

    public float health = 1;

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

    public void Defeated()
    {
        Destroy(gameObject);
    }
}
