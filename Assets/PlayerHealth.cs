using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject DeathMenu;
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
            Time.timeScale = 0f;
            DeathMenu.SetActive(true);
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

//using System.Collections;
//using UnityEngine;
//using UnityEngine.SceneManagement; // Added for potential scene management later

//public class PlayerHealth : MonoBehaviour
//{
//    public int maxHealth = 3;
//    private int currentHealth;

//    public HealthUI healthUI; // Assumed to be assigned in Inspector

//    private SpriteRenderer spriteRenderer;

//    private bool isInvulnerable = false;
//    private float invulnerabilityDuration = 1f; // how long you stay invincible after being hit

//    private bool isTouchingEnemy = false;
//    private float damageCooldown = 1f;
//    private float lastDamageTime = -Mathf.Infinity;

//    // --- Added for Death/Respawn Logic ---
//    private bool isDead = false;          // Prevent death logic from running multiple times
//    public float respawnDelay = 2f;       // Delay before respawning
//    public Transform respawnPoint;      // Assign a GameObject's Transform in the Inspector for the respawn location
//    // Add references to components to disable on death (Assign in Inspector or find in Start)
//    // e.g., public PlayerMovement playerMovement;
//    // e.g., public Collider2D playerCollider;
//    // --- End of Added Variables ---


//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        currentHealth = maxHealth;
//        healthUI.SetMaxHearts(maxHealth);
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        isDead = false; // Ensure player starts alive

//        // --- Find components if not assigned in Inspector (Example) ---
//        // playerMovement = GetComponent<PlayerMovement>();
//        // playerCollider = GetComponent<Collider2D>();
//        if (respawnPoint == null)
//        {
//            Debug.LogWarning("Respawn Point not set for PlayerHealth. Player will respawn at origin.");
//        }
//        // --- End Finding Components ---
//    }

//    private void Update()
//    {
//        // Optimization: Don't check for damage if dead
//        if (isDead) return;

//        if (isTouchingEnemy && Time.time - lastDamageTime > damageCooldown)
//        {
//            // Pass damage directly to TakeDamage
//            TakeDamage(1);
//            // The cooldown logic is now implicitly handled by invulnerability,
//            // but we keep lastDamageTime to prevent immediate re-triggering
//            // in the same frame if invulnerability was extremely short.
//            lastDamageTime = Time.time;
//        }
//    }


//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        // Optimization: Don't check for collisions if dead
//        if (isDead) return;

//        // Use CompareTag for efficiency and robustness instead of GetComponent if possible
//        // Assign the "Enemy" tag to your enemy prefabs/GameObjects
//        if (collision.gameObject.CompareTag("Enemy")) // Recommended: Use Tags
//        // Alternatively, keep GetComponent if EnemyMovement is the defining factor:
//        // if (collision.gameObject.GetComponent<EnemyMovement>())
//        {
//            isTouchingEnemy = true;
//            // Trigger damage immediately on touch if not on cooldown/invulnerable
//            // This makes collision feel more responsive than waiting for Update
//            if (Time.time - lastDamageTime > damageCooldown)
//            {
//                TakeDamage(1);
//                lastDamageTime = Time.time;
//            }
//        }
//    }

//    private void OnCollisionExit2D(Collision2D collision)
//    {
//        // Use CompareTag for efficiency and robustness
//        if (collision.gameObject.CompareTag("Enemy")) // Recommended: Use Tags
//        // Alternatively, keep GetComponent:
//        // if (collision.gameObject.GetComponent<EnemyMovement>())
//        {
//            isTouchingEnemy = false;
//        }
//    }


//    private void TakeDamage(int damage)
//    {
//        // Already checked isDead in Update/OnCollisionEnter, but keep check here for external calls
//        if (isInvulnerable || isDead)
//            return;

//        currentHealth -= damage;
//        // Clamp health just in case it visually goes below 0 on UI
//        healthUI.UpdateHearts(Mathf.Max(currentHealth, 0));
//        StartCoroutine(FlashRed());
//        StartCoroutine(InvulnerabilityCoroutine());

//        // --- Start of Modified Section ---
//        if (currentHealth <= 0)
//        {
//            Debug.Log("Player Dead");
//            // --- Add death logic here ---

//            // Check the isDead flag HERE to prevent repeated calls from this point
//            if (!isDead)
//            {
//                Die(); // Call the dedicated death function
//            }

//            // --- End of added logic ---
//        }
//        // --- End of Modified Section ---
//    }

//    // --- Added Methods Start Here ---

//    void Die()
//    {
//        isDead = true; // Set the flag *first*

//        // Stop ongoing coroutines that might interfere (like invulnerability flashing)
//        StopCoroutine(InvulnerabilityCoroutine());
//        StopCoroutine(FlashRed()); // Stop flashing red if it was ongoing
//        spriteRenderer.color = Color.white; // Ensure color is reset
//        spriteRenderer.enabled = true;     // Ensure sprite is visible (or set to a specific death sprite/animation)

//        // 1. Disable Player Controls and Interaction
//        // You NEED to add references to your specific components
//        // GetComponent<PlayerMovement>().enabled = false; // Example
//        // GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Stop movement
//        // GetComponent<Rigidbody2D>().isKinematic = true; // Optional: Stop physics interaction
//        // GetComponent<Collider2D>().enabled = false; // Stop collisions

//        Debug.Log("Disabling player components (Add specific components here)");


//        // 2. Play Death Animation or Effect (Optional)
//        // if (playerAnimator != null) playerAnimator.SetTrigger("Die");
//        // if (deathEffectPrefab != null) Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);


//        // 3. Start Respawn Timer
//        Invoke("Respawn", respawnDelay); // Call Respawn method after 'respawnDelay' seconds
//    }

//    void Respawn()
//    {
//        // 1. Reset Position
//        if (respawnPoint != null)
//        {
//            transform.position = respawnPoint.position;
//            transform.rotation = respawnPoint.rotation; // Optional: Reset rotation
//        }
//        else
//        {
//            // Fallback if no respawn point is set
//            transform.position = Vector3.zero;
//            transform.rotation = Quaternion.identity;
//        }

//        // 2. Reset Health
//        currentHealth = maxHealth;
//        healthUI.UpdateHearts(currentHealth); // Update UI

//        // 3. Reset State Flags
//        isDead = false;
//        isInvulnerable = false; // Ensure not invulnerable on respawn
//        isTouchingEnemy = false; // Reset enemy contact flag
//        lastDamageTime = -Mathf.Infinity; // Reset damage timer


//        // 4. Re-enable Player Controls and Interaction
//        // Make sure sprite is visible and normal color
//        spriteRenderer.enabled = true;
//        spriteRenderer.color = Color.white;

//        // Re-enable the components you disabled in Die()
//        // GetComponent<PlayerMovement>().enabled = true; // Example
//        // GetComponent<Rigidbody2D>().isKinematic = false; // If you made it kinematic
//        // GetComponent<Collider2D>().enabled = true;

//        Debug.Log("Player Respawned. Re-enabling player components (Add specific components here)");

//        // 5. Optional: Brief invulnerability on respawn
//        StartCoroutine(InvulnerabilityCoroutine());
//    }


//    // --- Existing Methods Continue Below ---


//    private IEnumerator FlashRed()
//    {
//        // Check if dead to prevent flashing after death starts
//        if (isDead) yield break;
//        spriteRenderer.color = Color.red;
//        yield return new WaitForSeconds(0.2f);
//        // Check again in case died during the wait
//        if (!isDead) spriteRenderer.color = Color.white;
//    }

//    private IEnumerator InvulnerabilityCoroutine()
//    {
//        isInvulnerable = true;

//        float elapsed = 0f;
//        float blinkInterval = 0.1f; // How fast to blink

//        while (elapsed < invulnerabilityDuration && !isDead) // Added !isDead check
//        {
//            // Toggle visibility
//            spriteRenderer.enabled = !spriteRenderer.enabled;

//            // Wait a short time before next blink
//            yield return new WaitForSeconds(blinkInterval);

//            elapsed += blinkInterval;
//        }

//        // Ensure sprite is visible and invulnerability off, ONLY if not dead
//        if (!isDead)
//        {
//            spriteRenderer.enabled = true;
//            isInvulnerable = false;
//        }
//        // If player died during invulnerability, Die() handles the final state.
//    }


//    public void Heal(int amount)
//    {
//        // Cannot heal if dead
//        if (isDead) return;

//        currentHealth += amount;
//        if (currentHealth > maxHealth)
//        {
//            currentHealth = maxHealth;
//        }

//        healthUI.UpdateHearts(currentHealth);
//    }

//    public void IncreaseMaxHealth(int amount)
//    {
//        // Allow increasing max health even if dead, might be relevant for upgrades
//        maxHealth += amount;
//        // Update UI to show the new maximum capacity
//        healthUI.SetMaxHearts(maxHealth);
//        // Optionally heal to the new max, or just increase capacity
//        // Heal(amount); // Uncomment this if gaining max health should also heal
//    }
//}

