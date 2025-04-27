using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickupUIController : MonoBehaviour
{
    public static ItemPickupUIController Instance { get; private set; }

    public GameObject popupPrefab;
    public int maxPopups = 5;
    public float popupDuration = 3f;

    private readonly Queue<GameObject> activePopups = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple ItemPickupUIManager instances detected! Destroying the extra one.");
            Destroy(gameObject);
        }
    }

    public void ShowItemPickup(string itemName, Sprite itemIcon)
    {
        GameObject newPopup = Instantiate(popupPrefab, transform);
        newPopup.GetComponentInChildren<TMP_Text>().text = itemName;

        Image itemImage = newPopup.transform.Find("ItemIcon").GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = itemIcon;
        }

        activePopups.Enqueue(newPopup);

        if (activePopups.Count > maxPopups)
        {
            GameObject oldestPopup = activePopups.Dequeue();
            Destroy(oldestPopup);
        }

        StartCoroutine(FadeOutAndDestroy(newPopup));
    }

    private IEnumerator FadeOutAndDestroy(GameObject popup)
    {
        yield return new WaitForSeconds(popupDuration);
        if (popup == null) yield break;

        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        if (canvasGroup == null) yield break;

        float fadeDuration = 0.5f;
        float timePassed = 0f;

        while (timePassed < fadeDuration)
        {
            if (popup == null) yield break;

            timePassed += Time.deltaTime;
            canvasGroup.alpha = 1f - (timePassed / fadeDuration);
            yield return null;
        }

        if (activePopups.Contains(popup))
        {
            activePopups.Dequeue();
        }

        Destroy(popup);
    }
}