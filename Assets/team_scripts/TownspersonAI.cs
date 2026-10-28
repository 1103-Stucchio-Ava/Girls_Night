
using UnityEngine;

public class TownspersonAI : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float waypointDistance = 0.15f;
    [SerializeField] private Transform flashlightBeam;

    private int currentWaypoint = 0;

    void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Transform target = patrolPoints[currentWaypoint];

        if (target == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction =
            (Vector2)target.position - rb.position;

        if (direction.magnitude <= waypointDistance)
        {
            currentWaypoint =
                (currentWaypoint + 1) % patrolPoints.Length;

            target = patrolPoints[currentWaypoint];

            if (target == null)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            direction = (Vector2)target.position - rb.position;
        }

        if (direction.sqrMagnitude > 0.001f)
        {
            rb.linearVelocity = direction.normalized * patrolSpeed;

            if (flashlightBeam != null)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                flashlightBeam.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
