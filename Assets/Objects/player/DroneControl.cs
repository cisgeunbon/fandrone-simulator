using System;
using JetBrains.Annotations;
using UnityEditor.Callbacks;
using UnityEngine;

public class DroneControl : MonoBehaviour
{

    [SerializeField] private static float fanThrust = 10f;
    [SerializeField] private Transform frontFanPoint;
    [SerializeField] private Transform rearFanPoint;
    public bool frontFanOn;
    public bool rearFanOn;

    public Vector2 frontThrust = new Vector2();
    public Vector2 rearThrust = new Vector2();
    public Vector2 droneUp = new Vector2(0f, 1f);


    [SerializeField] private GameObject Grabber; public Rigidbody2D rb;
    private FixedJoint2D pickupJoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        frontFanOn = Input.GetKey(KeyCode.RightArrow);
        rearFanOn = Input.GetKey(KeyCode.LeftArrow);
        if (Input.GetKey(KeyCode.Z))
        {
            releaseItem();
        }


    }

    void FixedUpdate()
    {
        droneUp = transform.up;

        if (frontFanOn)
        {
            //frontThrust = fanThrust;
            rb.AddForceAtPosition(droneUp * fanThrust, frontFanPoint.position);
        }
        if (rearFanOn)
        {
            //rearThrust = fanThrust;
            rb.AddForceAtPosition(droneUp * fanThrust, rearFanPoint.position);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("in");
        if (pickupJoint != null)
        {
            return;
        }

        Rigidbody2D itemBody = other.attachedRigidbody;

        if (itemBody == null || !(itemBody.CompareTag("Pickup") || itemBody.CompareTag("Cargo")))
        {
            Debug.Log("not abnle");
            if (itemBody == null) Debug.Log("itemnull");
            if (!itemBody.CompareTag("Pickup")) Debug.Log("nottag");
            return;
        }
        Debug.Log("able");
        pickupJoint = itemBody.gameObject.AddComponent<FixedJoint2D>();
        pickupJoint.connectedBody = GetComponent<Rigidbody2D>();
        pickupJoint.enableCollision = false;

    }

    private void releaseItem()
    {
        if (!pickupJoint) return;
        Destroy(pickupJoint);
    }
}
