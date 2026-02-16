using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Réglages")]
    public float speed = 2f;
    public int damageAmount = 10;
    public float attackRate = 1.5f;
    public Animator anim;

    private Transform player;
    private float nextAttackTime = 0f;

    // Variables pour le recul
    private Rigidbody2D rb;
    private bool isKnockedBack = false;
    private float knockbackTimer = 0f;
    public float knockbackDuration = 0.2f; // Temps pendant lequel il est repoussé

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                isKnockedBack = false;
                rb.linearVelocity = Vector2.zero; //On arrête le mouvement de recul
            }
            return;
        }

        if (player != null) MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > 0.6f)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (direction.x > 0.1f) transform.localScale = new Vector3(1, 1, 1);
            else if (direction.x < -0.1f) transform.localScale = new Vector3(-1, 1, 1);

            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
            if (Time.time >= nextAttackTime)
            {
                AttackPlayer();
                nextAttackTime = Time.time + attackRate;
            }
        }
    }

    // Fonction appelée par le joueur pour pousser l'ennemi
    public void ApplyKnockback(Vector2 force)
    {
        if (rb == null) return;

        isKnockedBack = true;
        knockbackTimer = knockbackDuration;

        // On applique la force de recul
        rb.linearVelocity = Vector2.zero; //On reset la vitesse actuelle
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    void AttackPlayer()
    {
        anim.SetTrigger("Attack");
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null) playerHealth.TakeDamage(damageAmount);
    }
}