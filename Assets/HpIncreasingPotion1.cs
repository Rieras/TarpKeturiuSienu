using UnityEngine;

public class HPIncreasingPotion : Item
{
    public override void UseItem()
    {
        base.UseItem(); // optional: logs the item use

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.IncreaseMaxHealth(1); // +1 max heart
            playerHealth.Heal(1); // heal 1 heart (the new one)
        }
    }
}
