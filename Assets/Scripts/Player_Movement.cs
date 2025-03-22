using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 2f;
    bool isDashing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        rb.linearVelocity = moveInput * moveSpeed;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
        moveInput = new Vector2(moveInput.x, moveInput.y).normalized;
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);
        moveInput = context.ReadValue<Vector2>();

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        rb.linearVelocity = new Vector2(moveInput.x * dashSpeed, moveInput.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }
}