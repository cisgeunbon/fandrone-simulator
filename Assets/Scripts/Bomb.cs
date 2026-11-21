using UnityEditor.UI;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isActive;
    private GameObject obj;
    [SerializeField] private float blastStrength = 10f;

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive)
        {
            return;
        }

    }

    private void detonate()
    {

        //obj.destroy;
    }
}
