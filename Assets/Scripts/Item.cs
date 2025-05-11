using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public string Name;
    [Tooltip("Jei norite priskirti sprite rankiniu būdu")]
    public Sprite manualSprite;

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

