using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    public int xpValue = 50;        // Valeur d'XP de ce cristal
    public float collectDistance = 0.15f; // Distance de ramassage
    public float magnetSpeed = 5f;  // Vitesse d'attraction

    private Transform player;
    private bool isBeingCollected = false;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Si le joueur est assez proche, le cristal commence à voler vers lui
        if (distance < 3f) // Portée
        {
            isBeingCollected = true;
        }

        if (isBeingCollected)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, magnetSpeed * Time.deltaTime);

            // Si on touche le joueur
            if (distance < collectDistance)
            {
                Collect();
            }
        }
    }

    void Collect()
    {
        LevelSystem ls = player.GetComponent<LevelSystem>();
        if (ls != null)
        {
            ls.AddExperience(xpValue);
        }
        Destroy(gameObject);
    }
}