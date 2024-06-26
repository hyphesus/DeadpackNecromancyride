using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target object to follow
    private ConstantMovement targetMovement;
    public Vector3 offset = new Vector3(0, 0, -10); // Offset from the target

    void Start()
    {
        if (target != null)
        {
            targetMovement = target.GetComponent<ConstantMovement>();
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Update the camera's position to follow the target on the x-axis with the specified offset
            transform.position = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
        }
    }
}
