using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public HealthUI healthUI;

    private SpriteRenderer spriteRenderer;

    private bool isInvulnerable = false;
    private float invulnerabilityDuration = 1f; // how long you stay invincible after being hit

    private bool isTouchingEnemy = false;
    private float damageCooldown = 1f;
    private float lastDamageTime = -Mathf.Infinity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isTouchingEnemy && Time.time - lastDamageTime > damageCooldown)
        {
            TakeDamage(1);
            lastDamageTime = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMovement>())
        {
            isTouchingEnemy = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMovement>())
        {
            isTouchingEnemy = false;
        }
    }

    private void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);
        StartCoroutine(FlashRed());
        StartCoroutine(InvulnerabilityCoroutine());

        if (currentHealth <= 0)
        {
            Debug.Log("Player Dead");
            // Add death logic here
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        float elapsed = 0f;
        bool isVisible = true;

        while (elapsed < invulnerabilityDuration)
        {
            // Toggle visibility
            isVisible = !isVisible;
            spriteRenderer.enabled = isVisible;

            // Wait a short time before next blink
            yield return new WaitForSeconds(0.1f);

            elapsed += 0.1f;
        }

        // Make sure sprite is visible again at the end
        spriteRenderer.enabled = true;
        isInvulnerable = false;
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthUI.UpdateHearts(currentHealth);
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        healthUI.SetMaxHearts(maxHealth);  // Update UI
    }
}
