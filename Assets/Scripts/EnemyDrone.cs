using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyDrone : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffectPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 10f;
    private Vector2 moveDirection = new Vector2();

    private Vector2 toPlayer = new Vector2();
    Vector2 dronePos = new Vector2();
    [SerializeField] private GameObject player;
    private Vector2 playerPos = new Vector2();

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private Rigidbody2D rb;
    private Transform target;
    private RaycastHit2D raycast;


    [SerializeField] private LayerMask occlusionLayers;

    public async Awaitable Death()
    {
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointB;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D detected = other.attachedRigidbody;
        if (detected == null || !detected.CompareTag("Player")) return;

        // raycast = Physics2D.Linecast(transform.position, detected.position, occlusionLayers);

        // if (raycast.collider != null) return;

        if (detected.CompareTag("Player"))
        {
            _ = detected.GetComponent<DroneControl>().Death();
        }
    }

    void FixedUpdate()
    {
        // bool blocked = Physics2D.Linecast(transform.position, playerPos, occlusionLayers).collider != null;
        // Vector2 targetPos = target.position;
        // detection = detection && !blocked;

        if (rb.position == (Vector2)target.position)
        {
            target = target == pointA ? pointB : pointA;
        }
        Vector2 nextPosition = Vector2.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime);

        rb.MovePosition(nextPosition);
    }
}
