using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [Header("Statistiques d'XP")]
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100; // Premier niveau à 100 XP (environ 2-3 ennemis pour gamedesign)

    // On ajoutera une barre d'XP UI plus tard

    public void AddExperience(int amount)
    {
        currentXP += amount;

        Debug.Log("XP gagnée ! Total : " + currentXP + "/" + xpToNextLevel);

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentLevel++;
        currentXP -= xpToNextLevel;

        // Augmentation de la difficulté du prochain niveau (+20%)
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.2f);

        Debug.Log("NIVEAU SUPÉRIEUR ! Niveau actuel : " + currentLevel);

        // ICI : On lancera plus tard le menu de choix des 4 améliorations
        // Time.timeScale = 0f; // Pour mettre le jeu en pause par exemple
    }
}