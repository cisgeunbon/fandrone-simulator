using System;
using UnityEngine;


public class BigFan : MonoBehaviour
{
    [SerializeField] Vector2 windDirection = new Vector2(1f, 0f);
    [SerializeField] float windStrength = 10f;
    [SerializeField] GameObject windZone;

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D body = collision.attachedRigidbody;
        Debug.Log($"감지: {collision.name}, Rigidbody2D: {body}, 타입: {body?.bodyType}");

        if (body == null || body.bodyType != RigidbodyType2D.Dynamic)
            return;

        Debug.Log($"힘 적용: {body.name}, force: {windDirection * windStrength}");
        body.AddForce(windDirection * windStrength);

        return;
    }
}