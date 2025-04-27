using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [Header("Item Settings")]
    public string itemName;                  // Potiono pavadinimas
    public Sprite itemIcon;                  // Potiono ikona
    public bool isConsumable = true;         // Ar potionas sunaikinamas po naudojimo?
    public float healAmount = 5f;            // Kiek šis potionas gydo, kai yra naudojamas

    // Šis metodas bus iškviečiamas, kai naudosite potioną
    public void Use()
    {
        if (itemName.Contains("potion"))
        {
            HealthManager healthManager = FindObjectOfType<HealthManager>();
            if (healthManager != null)
            {
                // Išveda į logą ir gydo žaidėją pagal healAmount
                Debug.Log("Healing player for " + healAmount + " HP with " + itemName + "!");
                healthManager.Heal(healAmount); // Iškviečia Heal metodą, kuris turi padidinti žaidėjo gyvybes
            }
            else
            {
                Debug.LogWarning("HealthManager not found in the scene!");
            }

            // Jei potionas yra consumable, sunaikiname jį po naudojimo
            if (isConsumable)
            {
                Destroy(gameObject);  // Potionas sunaikinamas po naudojimo
            }
        }
        else
        {
            Debug.LogWarning("This item has no use functionality assigned.");
        }
    }
}
