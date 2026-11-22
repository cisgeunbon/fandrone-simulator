using UnityEngine;

public class HazardZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D contact = other.attachedRigidbody;
        if (contact == null) return;
        if (contact.CompareTag("Player"))
        {
            _ = contact.GetComponent<DroneControl>().Death();
        }
    }
}