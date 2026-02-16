using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    // On ne glisse que l'image rouge ici
    public Image redFillImage;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }
    void Update()
    {
        // --- PETIT TEST ---
        // Appuie sur K en jeu pour perdre 10 PV
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
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
            // Calcul du remplissage entre 0 et 1
            redFillImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " est mort !");

        // Si c'est un ennemi, on le fait disparaître
        if (gameObject.CompareTag("Enemy"))
        {
            // Optionnel : Tu peux spawn de l'XP ici plus tard
            Destroy(gameObject);
            
        }
        else if (gameObject.CompareTag("Player"))
        {
            // Ici on mettra ton écran de Game Over
            Debug.Log("GAME OVER");
        }
    }
}