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
                Defeated(); // Drop item and destroy enemy when health is 0 or below
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

    [SerializeField] private GameObject itemDropPrefab; // Assignable item to drop in the Unity editor

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

        if (Mathf.Abs(_targetDirection.x) > Mathf.Abs(_targetDirection.y))
        {
            _currentDirection = _targetDirection.x > 0 ? MovementDirection.Right : MovementDirection.Left;
        }
        else
        {
            _currentDirection = _targetDirection.y > 0 ? MovementDirection.Up : MovementDirection.Down;
        }

        //_animator.SetInteger("Direction", (int)_currentDirection);
    }

    public void Defeated()
    {
        if (itemDropPrefab != null) // Check if itemDropPrefab is assigned
        {
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity); // Drop the item at the enemy's position
        }
        Destroy(gameObject); // Destroy the enemy
    }
}
