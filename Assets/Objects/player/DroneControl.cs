using System;
using JetBrains.Annotations;
using UnityEditor.Callbacks;
using UnityEngine;

public class DroneControl : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffectPrefab;

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
    GameManager gm;

    // Animators
    public Animator frontFanAnimator;
    public Animator rearFanAnimator;

    // SFX
    public AudioSource hitSFX;
    public AudioSource rearFanSFX;
    public AudioSource frontFanSFX;

    public async Awaitable Death()
    {
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        await Awaitable.WaitForSecondsAsync(2f);
        gm.RestartStage();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        frontFanOn = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        rearFanOn = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            frontFanAnimator.gameObject.SetActive(true);
            frontFanSFX.Play();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            rearFanAnimator.gameObject.SetActive(true);
            rearFanSFX.Play();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            frontFanAnimator.gameObject.SetActive(false);
            frontFanSFX.Stop();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            rearFanAnimator.gameObject.SetActive(false);
            rearFanSFX.Stop();
        }
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.DownArrow))
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