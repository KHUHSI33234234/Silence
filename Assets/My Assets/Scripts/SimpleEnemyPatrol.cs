
using UnityEngine;

public class SimpleEnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform[] patrolPoints;

    [Header("Movement")]
    public float moveSpeed = 1.5f;
    public float rotationSpeed = 5f;
    public float waitTime = 2f;

    private int currentPoint = 0;
    private float waitTimer = 0f;
    private bool waiting = false;
    private float startingHeight;

    void Start()
    {
        startingHeight = transform.position.y;
    }

    void Update()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        if (waiting)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                waiting = false;
                waitTimer = 0f;
            }

            return;
        }

        Transform target = patrolPoints[currentPoint];

        Vector3 destination = new Vector3(
            target.position.x,
            startingHeight,
            target.position.z
        );

        Vector3 direction = destination - transform.position;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            transform.position = Vector3.MoveTowards(
                transform.position,
                destination,
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
            waiting = true;
            waitTimer = 0f;

            currentPoint++;

            if (currentPoint >= patrolPoints.Length)
                currentPoint = 0;
        }
    }
}
