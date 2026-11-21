using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isActive;
    private GameObject obj;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float impulse = 10f;
    [SerializeField] private LayerMask affectedLayers;
    [SerializeField] private GameObject explosionEffect;

    private bool hasExploded;


    FixedJoint2D pickupJoint;



    // Update is called once per frame
    void Update()
    {

    }

    public void SetPickupJoint(FixedJoint2D joint)
    {
        pickupJoint = joint;
        return;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        detonate();
        if (!isActive)
        {
            return;
        }

    }


    [ContextMenu("Explode")]
    private void detonate()
    {
        if (hasExploded) return;
        hasExploded = true;

        Vector2 center = transform.position;
        Rigidbody2D ownBody = GetComponent<Rigidbody2D>();
        var pushedBodies = new HashSet<Rigidbody2D>();

        foreach (Collider2D hit in Physics2D.OverlapCircleAll(center, radius, affectedLayers))
        {
            Rigidbody2D body = hit.attachedRigidbody;

            if (body == null || body == ownBody || body.bodyType != RigidbodyType2D.Dynamic || !pushedBodies.Add(body))
            {
                continue;
            }

            Vector2 offset = body.worldCenterOfMass - center;
            Vector2 direction = offset == Vector2.zero ? Vector2.up : offset.normalized;

            body.AddForce(direction * impulse, ForceMode2D.Impulse);
        }
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        //obj.destroy;
    }
}
