using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 camPos = new Vector3();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }
}
