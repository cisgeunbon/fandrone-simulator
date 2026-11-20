using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField] GameManager gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D itemBody = other.attachedRigidbody;


        if (itemBody == null || !itemBody.CompareTag("Cargo"))
        {
            return;
        }
        gm.moveNext();

    }


}
