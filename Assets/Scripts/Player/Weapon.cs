using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Tooltip("How much damage this weapon deals on hit")]
    public int damage = 1;

    // Make sure your weapon's collider is set to “Is Trigger”
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // (Optional) you could do a tag‐check here:
        // if (!collision.CompareTag("Enemy")) return;

        // Try to get the Enemy component on whatever we hit
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            // If it has one, deal damage
            enemy.TakeDamage(damage);
        }
    }
}
