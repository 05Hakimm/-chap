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

        // Si l'objet qui a ce script est un Ennemi, on le supprime du jeu
        if (gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else
        {
            // Si c'est le joueur, on peut faire autre chose (Game Over, etc.)
            Debug.Log("Le joueur a perdu !");
        }
    }
}