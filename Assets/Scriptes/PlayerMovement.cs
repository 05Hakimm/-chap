using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb.gravityScale = 0f;
    }

    void Update()
    {
        // 1. Récupère les directions
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // 2. Gestion de l'animation (si on bouge en X ou en Y)
        bool isMoving = movement.magnitude > 0;
        anim.SetBool("isMoving", isMoving);

        // 3. Gestion du Flip (regarder à gauche ou à droite)
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false; // Regarde à droite (normal)
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true; // Regarde à gauche (miroir)
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}