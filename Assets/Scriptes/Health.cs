using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject xpPrefab;

    [Range(0f, 1f)]
    public float xpDropRate = 0.5f;

    //l'image de la barre rouge dedans
    public Image redFillImage;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateUI()
    {
        if (redFillImage != null)
        {
            // Calcul du remplissage entre 0 et 1 de la partie rouge de la barre de vie
            redFillImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            // L'XP ne tombe que si le calcul aléatoire est inférieur au taux de drop
            if (xpPrefab != null && Random.value <= xpDropRate)
            {
                Instantiate(xpPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Player"))
        {
            // Ici on mettra l'écran de Game Over
            Debug.Log("GAME OVER");
        }
    }
}