using UnityEngine;

public class HealthPotion : Item
{
    public override void UseItem()
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Healing player for 1 HP"); // <--- Add this for debug
            playerHealth.Heal(1);
        }
        else
        {
            Debug.LogWarning("PlayerHealth not found!");
        }
    }
}