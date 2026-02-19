using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    [Header("Statistiques d'XP")]
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    [Header("Interface")]
    public Image xpFillImage; // L'image de remplissage
    public Text levelText;    // Le texte du niveau
    public GameObject upgradePanel; // Le menu de choix d'améliorations

    void Start()
    {

        // On cache le menu de niveau supérieur au début
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }

        UpdateUI();
    }

    public void AddExperience(int amount)
    {
        currentXP += amount;

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    void LevelUp()
    {
        currentLevel++;
        currentXP -= xpToNextLevel;

        // Augmentation de l'XP requise pour le niveau suivant
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.2f);

        Debug.Log("NIVEAU SUPÉRIEUR !");

        // Ouvre le menu et met le jeu en pause
        OpenUpgradeMenu();

        UpdateUI();
    }

    void OpenUpgradeMenu()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true);
            Time.timeScale = 0f; // Pause le jeu
        }
    }

    // Cette fonction sera appelée par les boutons de ton menu d'amélioration
    public void CloseUpgradeMenu()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
            Time.timeScale = 1f; // Reprend le jeu
        }
    }

    void UpdateUI()
    {
        if (xpFillImage != null)
        {
            // Calcul du remplissage
            xpFillImage.fillAmount = (float)currentXP / xpToNextLevel;
        }

        if (levelText != null)
        {
            levelText.text = "NIVEAU " + currentLevel;
        }
    }
}