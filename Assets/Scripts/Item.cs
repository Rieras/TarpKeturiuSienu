using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public int ID;
    public string Name;
    public int quantity = 1;
    [Tooltip("Jei norite priskirti sprite rankiniu būdu")]
    public Sprite manualSprite;

    private TMP_Text quantityText;

    private void Awake()
    {
        quantityText = GetComponentInChildren<TMP_Text>();
        UpdateQuantityDisplay();
    }
    public void UpdateQuantityDisplay()
    {
        if (quantityText != null)
        {
            quantityText.text = quantity > 1 ? quantity.ToString() : "";
        }
    }

    public void AddToStack(int amount = 1)
    {
        quantity += amount;
        UpdateQuantityDisplay();
    }

    public int RemoveFromStack(int amount = 1)
    {
        int removed = Mathf.Min(amount, quantity);
        quantity -= removed;
        UpdateQuantityDisplay();
        return removed;
    }

    public GameObject CloneItem(int newQuantity)
    {
        GameObject clone = Instantiate(gameObject);
        Item cloneItem = clone.GetComponent<Item>();
        cloneItem.quantity = newQuantity;
        cloneItem.UpdateQuantityDisplay();
        return clone;
    }
    public virtual void UseItem()
    {
        Debug.Log("Using item " + Name);
    }

    public virtual void PickUp()
    {
        // Pataisytas Image gavimas su null check
        var imageComponent = GetComponent<UnityEngine.UI.Image>();
        if (imageComponent != null)
        {
            Sprite itemIcon = imageComponent.sprite;

            if (ItemPickupUIController.Instance != null)
            {
                ItemPickupUIController.Instance.ShowItemPickup(Name, itemIcon);
            }
        }
        else
        {
            Debug.LogError("Nerastas Image komponentas!", this);
        }
    }
}
//public virtual void PickUp()
//{
//    // Correct way to get the Image component:
//    Image ImgComponent = GetComponent<Image>();
//    if (ImgComponent == null)
//    {
//        Debug.LogError("No Image component found on this GameObject!");
//        return;
//    }

//    Sprite ItemIcon = ImgComponent.sprite;
//    if (ItemPickupUIController.Instance != null)
//    {
//        ItemPickupUIController.Instance.ShowItemPickup(Name, ItemIcon);
//    }
//}

