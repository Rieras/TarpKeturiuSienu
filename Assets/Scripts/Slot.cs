using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject currentItem;

    public void UpdateSlotUI()
    {
        currentItem = null;  // Clears the slot from holding any reference
                             // Optionally reset visuals here, like turning off a visual indicator that the slot is filled.
    }
}
