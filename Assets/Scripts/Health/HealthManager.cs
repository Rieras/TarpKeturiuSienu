using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Pridedame scenų valdymą

public class HealthManager : MonoBehaviour
{
    [SerializeField] public Image healthBar;
    public float healthAmount = 100f;

    void Update()
    {
        if (healthAmount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Perkrauna sceną
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(5);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); // Užtikriname, kad reikšmė neviršytų ribų
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); // Užtikriname, kad reikšmė neviršytų ribų
        healthBar.fillAmount = healthAmount / 100f;
    }
}
