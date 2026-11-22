using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyDrone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 10f;
    public bool detection = false;
    private Vector2 moveDirection = new Vector2();

    [SerializeField] public GameManager gm;

    private Vector2 toPlayer = new Vector2();
    Vector2 dronePos = new Vector2();
    [SerializeField] private GameObject player;
    private Vector2 playerPos = new Vector2();

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private Rigidbody2D rb;
    private Transform target;


    [SerializeField] private LayerMask occlusionLayers;


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

        if (detected == null || !detected.CompareTag("Player"))
        {
            detection = false;
            return;
        }

        detection = true;
    }

    void FixedUpdate()
    {
        // bool blocked = Physics2D.Linecast(transform.position, playerPos, occlusionLayers).collider != null;
        // Vector2 targetPos = target.position;
        // detection = detection && !blocked;

        if (detection)
        {
            gm.RestartStage();
        }
        if (!detection && rb.position == (Vector2)target.position)
        {
            target = target == pointA ? pointB : pointA;
        }
        Vector2 nextPosition = Vector2.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime);

        rb.MovePosition(nextPosition);
    }
}
