using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Paramètres d'Attaque")]
    public Animator anim;
    public float attackRate = 3f;    // Temps entre chaque salve
    public int attackCount = 1;      // Nombre de coups/projectiles par salve 
    public float delayBetweenShots = 0.2f; // Petit délai entre 2 coups d'une même salve

    private float nextAttackTime = 0f;
    private int comboStep = 0;

    void Update()
    {
        // On attaque automatiquement selon le cooldown
        if (Time.time >= nextAttackTime)
        {
            StartCoroutine(PerformAttackSequence());
            nextAttackTime = Time.time + attackRate;
        }
    }

    // Ce bloc gère la salve d'attaques (1, 2, 3 coups ou plus)
    System.Collections.IEnumerator PerformAttackSequence()
    {
        for (int i = 0; i < attackCount; i++)
        {
            ExecuteOneAttack();

            // On attend un tout petit peu avant le prochain coup de la salve
            yield return new WaitForSeconds(delayBetweenShots);
        }
    }

    void ExecuteOneAttack()
    {
        //Animation de corps à corps (ton épée)
        anim.SetInteger("AttackIndex", comboStep);
        anim.SetTrigger("Attack");

        // Alterne entre l'animation 1 et 2
        comboStep = (comboStep == 0) ? 1 : 0;

        // ici on ajoutera les boules de feu plus tard (et le reste)
        LaunchFireball();

        Debug.Log("Attaque exécutée !");
    }

    void LaunchFireball()
    {
        // Pour l'instant c'est vide, on y mettra le code des projectiles
        // quand tu auras créé un prefab de boule de feu.
        // et on fera d'autres fonctions pour d'autres pouvoirs.
    }
}