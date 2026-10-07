using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 2.5f;
    public KeyCode attackKey = KeyCode.Mouse0;
    public float damagePerHit = 100f;

    public Animator playerAnimator;

    private NavMeshAgent agent;
    private EnemyPatrol patrol;
    private bool attacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrol = GetComponent<EnemyPatrol>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");

            if (p != null)
            {
                player = p.transform;

                if (playerAnimator == null)
                    playerAnimator = p.GetComponentInChildren<Animator>();
            }
        }
    }

    void Update()
    {
        if (player == null || attacking)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && Input.GetKeyDown(attackKey))
        {
            StartCoroutine(Attack());
        }
    }

    System.Collections.IEnumerator Attack()
    {
        attacking = true;

        // STOP THE ENEMY
        if (agent != null)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        // STOP PATROL SCRIPT
        if (patrol != null)
        {
            patrol.enabled = false;
        }

        // PLAYER ATTACK ANIMATION
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Attack");
        }

        // Wait
        yield return new WaitForSeconds(0.3f);

        // DAMAGE PLAYER
        if (PlayerHealth.Instance != null)
        {
            PlayerHealth.Instance.TakeDamage(damagePerHit);
        }

        // Stay stopped
        yield return new WaitForSeconds(1f);

        // START MOVING AGAIN
        if (agent != null)
        {
            agent.isStopped = false;
        }

        if (patrol != null)
        {
            patrol.enabled = true;
        }

        attacking = false;
    }
}