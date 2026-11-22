using UnityEditor.Callbacks;
using UnityEngine;

public class HazardZone : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D contact = other.attachedRigidbody;
        if (contact.CompareTag("Player"))
        {
            _ = contact.GetComponent<DroneControl>().Death();
        }
    }
}