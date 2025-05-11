using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject currentItem;

    public void UpdateSlotUI()
    {
        if (currentItem != null)
        {
            Item item = currentItem.GetComponent<Item>();
            item?.UpdateQuantityDisplay();
        }
    }
}
