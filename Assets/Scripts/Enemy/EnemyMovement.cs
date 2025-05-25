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
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
            _animator.SetBool("isMoving", false); // ✅ THIS IS MISSING IN YOUR SCRIPT
            return;
        }

        _rigidbody.linearVelocity = _targetDirection.normalized * _speed;
        _animator.SetBool("isMoving", true); // ✅ THIS IS ALSO MISSING IN YOUR SCRIPT
    }

    private Vector2 _lastMoveDirection = Vector2.down;
    private void UpdateAnimationDirection()
    {
        if (_targetDirection != Vector2.zero)
        {
            _lastMoveDirection = _targetDirection.normalized;

            if (Mathf.Abs(_lastMoveDirection.x) > Mathf.Abs(_lastMoveDirection.y))
            {
                _spriteRenderer.flipX = _lastMoveDirection.x < 0;
            }
        }

        _animator.SetFloat("MoveX", _lastMoveDirection.x);
        _animator.SetFloat("MoveY", _lastMoveDirection.y);
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
