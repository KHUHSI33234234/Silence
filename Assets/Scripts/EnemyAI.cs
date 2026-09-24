using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 2.5f;
    public KeyCode attackKey = KeyCode.Mouse0;
    public float damagePerHit = 20f;

    public Renderer enemyRenderer;
    public Color flashColor = Color.red;
    public float flashDuration = 0.15f;

    private Color originalColor;

    void Start()
    {
        if (enemyRenderer != null) originalColor = enemyRenderer.material.color;

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (player == null) return;
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= attackRange && Input.GetKeyDown(attackKey))
            Engage();
    }

    void Engage()
    {
        Debug.Log(gameObject.name + " engaged. Player takes damage.");

        if (PlayerHealth.Instance != null)
            PlayerHealth.Instance.TakeDamage(damagePerHit);

        if (enemyRenderer != null)
            StartCoroutine(FlashDamageColor());
    }

    System.Collections.IEnumerator FlashDamageColor()
    {
        enemyRenderer.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        enemyRenderer.material.color = originalColor;
    }
}