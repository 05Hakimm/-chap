using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Paramètres d'Attaque")]
    public Animator anim;
    public float attackRate = 3f;
    public int attackCount = 1;
    public float delayBetweenShots = 0.2f;

    [Header("Stats de Combat")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float knockbackForce = 5f; 
    public LayerMask enemyLayers;

    private float nextAttackTime = 0f;
    private int comboStep = 0;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            StartCoroutine(PerformAttackSequence());
            nextAttackTime = Time.time + attackRate;
        }
    }

    IEnumerator PerformAttackSequence()
    {
        for (int i = 0; i < attackCount; i++)
        {
            ExecuteOneAttack();
            yield return new WaitForSeconds(delayBetweenShots);
        }
    }

    void ExecuteOneAttack()
    {
        anim.SetInteger("AttackIndex", comboStep);
        anim.SetTrigger("Attack");
        comboStep = (comboStep == 0) ? 1 : 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // 1. Dégâts
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null) enemyHealth.TakeDamage(attackDamage);

            // 2. Recul (Knockback)
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                // On calcule la direction du recul (du joueur vers l'ennemi)
                Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                enemyAI.ApplyKnockback(knockbackDirection * knockbackForce);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}