using UnityEngine;

public class EnemyDrone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool detection = false;
    private Vector2 toDrone = new Vector2();


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        toDrone = Vector2(0f, 1f);
        if (detection == true)
        {
            Debug.Log("gameover");
        }
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
}
