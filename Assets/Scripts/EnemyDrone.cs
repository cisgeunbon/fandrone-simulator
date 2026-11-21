using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDrone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 10f;
    public bool detection = false;
    private Vector2 moveDirection = new Vector2();


    private Vector2 toPlayer = new Vector2();
    Vector2 dronePos = new Vector2();
    private GameObject player;
    private Vector2 playerPos = new Vector2();

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private Rigidbody2D rb;
    private Transform target;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointB;
    }

    // Update is called once per frame
    void Update()
    {


        //toDrone = Vector2(0f, 1f);

    }



    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D detected = other.attachedRigidbody;

        if (detected == null || !CompareTag("Drone"))
        {
            detection = false;
            return;
        }

        detection = true;
    }

    void FixedUpdate()
    {
        Vector2 targetPos = target.position;
        // if (detection)
        // {
        //     target = player.transform;
        // }
        if (rb.position == targetPos)
        {
            target = target == pointA ? pointB : pointA;
        }
        Vector2 nextPosition = Vector2.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime);

        rb.MovePosition(nextPosition);
    }
}
