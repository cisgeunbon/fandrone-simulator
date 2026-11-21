using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public BackgroundController backgroundController;
    public Transform target;
    private Vector3 camPos = new Vector3();

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10);
        backgroundController.UpdateBackground();
    }
}
