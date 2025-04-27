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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // While dashing we override normal velocity
        if (isDashing) return;

        // Normal move
        rb.linearVelocity = moveInput * moveSpeed;

        // Dash on LeftShift (you can wire this to an InputAction too)
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
            StartCoroutine(Dash());
    }

    // This method is called by your InputAction (Make sure your Player Input component
    // is set up to call "Move" on Value / Vector2).
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        // Animation flags
        bool walking = moveInput != Vector2.zero;
        animator.SetBool("isWalking", walking);

        // Remember last direction for idle
        if (context.canceled)
        {
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        // Feed current direction into the blend tree
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        rb.linearVelocity = moveInput * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

}
