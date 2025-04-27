using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Melee Hitbox/GameObject")]
    public GameObject Melee;           // Drag your melee‐hitbox GameObject here

    [Header("Attack Timings")]
    public float atkDuration = 0.3f;   // How long the hitbox stays active
    private float atkTimer = 0f;
    private bool isAttacking = false;

    void Update()
    {
        // 1) Tick down our attack timer and disable melee when it expires
        //CheckMeleeTimer();

        // 2) If not already attacking, listen for the attack key or mouse‐click
        if (!isAttacking && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
        {
            OnAttack();
        }
    }

    /// <summary>
    /// Starts the melee attack: activates the hitbox, resets timer.
    /// </summary>
    void OnAttack()
    {
        Melee.SetActive(true);
        isAttacking = true;
        atkTimer = atkDuration;

        // --- Optional: trigger an Animator here ---
        // GetComponent<Animator>()?.SetTrigger("Attack");
    }

    /// <summary>
    /// Runs every frame to disable the hitbox when atkTimer hits zero.
    /// </summary>
    void CheckMeleeTimer()
    {
        if (!isAttacking) return;

        atkTimer -= Time.deltaTime;
        if (atkTimer <= 0f)
        {
            isAttacking = false;
            Melee.SetActive(false);
        }
    }
}
