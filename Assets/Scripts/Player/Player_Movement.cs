using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    [Header("Dash")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 0.2f;
    private bool isDashing;

    [Header("Animation")]
    private Animator animator;
    private Vector2 moveInput;

    [Header("Aiming")]
    public Transform Aim; // assign your Aim transform in the Inspector

    [Header("Sprite")]
    private SpriteRenderer spriteRenderer;

    public swordAttack swordAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // make sure your SpriteRenderer is on the same GameObject
    }

    void Update()
    {
        if (isDashing) return;

        // Movement
        rb.linearVelocity = moveInput * moveSpeed;

        // Dash
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
            StartCoroutine(Dash());

        // Flip sprite based on horizontal movement
        if (moveInput.x != 0)
        {
            spriteRenderer.flipX = moveInput.x < 0;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    public void Attack(InputAction.CallbackContext context)
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    {
        float lastX = animator.GetFloat("LastInputX");
        float lastY = animator.GetFloat("LastInputY");

        if (Mathf.Abs(lastX) > Mathf.Abs(lastY))
        {
            if (lastX > 0)
                swordAttack.AttackRight();
            else
                swordAttack.AttackLeft();
        }
        else
        {
            if (lastY > 0)
                swordAttack.AttackUp();
            else
                swordAttack.AttackDown();
        }
    }

    public void EndSwordAttack()
    {
        swordAttack.StopAttack();
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        rb.linearVelocity = moveInput * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }
}
