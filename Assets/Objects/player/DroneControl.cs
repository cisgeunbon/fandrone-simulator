using System;
using JetBrains.Annotations;
using UnityEditor.Callbacks;
using UnityEngine;

public class DroneControl : MonoBehaviour
{

    [SerializeField] private float maxFanThrust = 20f;
    [SerializeField] private float thrustAcceleration = 30f;
    [SerializeField] private float thrustDeceleration = 40f;
    [SerializeField] private float stabilizationStrength = 5f;
    [SerializeField] private float rotationalDamping = 2f;
    [SerializeField] private float maxCorrectionTorque = 10f;
    [SerializeField] private float horizontalDamping = 4f;
    [SerializeField] private float maxHorizontalSpeed = 15f;


    float frontThrust;
    float rearThrust;

    [SerializeField] private static float fanThrust = 10f;
    [SerializeField] private Transform frontFanPoint;
    [SerializeField] private Transform rearFanPoint;
    public bool frontFanOn;
    public bool rearFanOn;

    public Vector2 droneUp = new Vector2(0f, 1f);


    [SerializeField] private GameObject Grabber; public Rigidbody2D rb;
    private FixedJoint2D pickupJoint;


    // SFX
    public AudioSource hitSFX;
    public AudioSource rearFanSFX;
    public AudioSource frontFanSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        frontFanOn = Input.GetKey(KeyCode.LeftArrow);
        rearFanOn = Input.GetKey(KeyCode.RightArrow);
        if (Input.GetKey(KeyCode.Z))
        {
            releaseItem();
        }


    }

    void FixedUpdate()
    {
        droneUp = transform.up;

        float frontTarget = frontFanOn ? maxFanThrust : 0f;
        float rearTarget = rearFanOn ? maxFanThrust : 0f;

        frontThrust = Mathf.MoveTowards(
            frontThrust,
            frontTarget,
            (frontFanOn ? thrustAcceleration : thrustDeceleration) * Time.fixedDeltaTime
        );

        rearThrust = Mathf.MoveTowards(
            rearThrust,
            rearTarget,
            (rearFanOn ? thrustAcceleration : thrustDeceleration) * Time.fixedDeltaTime
        );

        rb.AddForceAtPosition(droneUp * frontThrust, frontFanPoint.position);
        rb.AddForceAtPosition(droneUp * rearThrust, rearFanPoint.position);

        // auto stabilization
        float angleError = Mathf.DeltaAngle(transform.eulerAngles.z, 0f);

        float correction = angleError * stabilizationStrength
            - rb.angularVelocity * rotationalDamping;

        correction = Mathf.Clamp(correction, -maxCorrectionTorque, maxCorrectionTorque);

        rb.AddTorque(correction);

        // horizontal damping and limit
        Vector2 velocity = rb.linearVelocity;
        velocity.x *= Mathf.Exp(-horizontalDamping * Time.fixedDeltaTime);
        velocity.x = Mathf.Clamp(velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
        rb.linearVelocity = velocity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (pickupJoint != null)
        {
            return;
        }

        Rigidbody2D itemBody = other.attachedRigidbody;

        if (itemBody == null || itemBody == rb) return;

        if (!itemBody.CompareTag("Pickup") && !itemBody.CompareTag("Cargo")) return;

        pickupJoint = gameObject.AddComponent<FixedJoint2D>();
        pickupJoint.connectedBody = itemBody;
        pickupJoint.enableCollision = false;

        Cargo cargo = itemBody.GetComponent<Cargo>();


        if (cargo != null)
        {
            cargo.SetPickupJoint(pickupJoint);
        }

        // if (itemBody == null || !(itemBody.CompareTag("Pickup") || itemBody.CompareTag("Cargo")))
        // {
        //     Debug.Log("itembody null");
        //     // if (itemBody == null) Debug.Log("itemnull");
        //     // if (!itemBody.CompareTag("Pickup")) Debug.Log("nottag");
        //     return;
        // }
        // Debug.Log("able");
        // pickupJoint = itemBody.gameObject.AddComponent<FixedJoint2D>();
        // pickupJoint.connectedBody = GetComponent<Rigidbody2D>();
        // pickupJoint.enableCollision = false;

    }
    private void releaseItem()
    {
        if (pickupJoint == null)
            return;

        Rigidbody2D itemBody = pickupJoint.connectedBody;
        if (itemBody != null && itemBody.TryGetComponent<Cargo>(out var cargo))
        {
            cargo.SetPickupJoint(null);
            cargo.isActive = true;
        }
        pickupJoint.enabled = false;
        Destroy(pickupJoint);
        pickupJoint = null;
    }
}