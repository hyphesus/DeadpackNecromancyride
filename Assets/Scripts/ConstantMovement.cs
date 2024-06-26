using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    public float initialSpeed = 2f; // Initial speed at which the object starts moving
    public float speedIncreaseRate = 0.1f; // Rate at which the speed increases over time
    public float moveSpeed { get; private set; } // Make moveSpeed publicly readable
    private bool isMoving = true;

    void Start()
    {
        moveSpeed = initialSpeed;
    }

    void Update()
    {
        if (isMoving)
        {
            // Move the object to the right with an increasing speed
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            // Increase the speed exponentially
            moveSpeed += speedIncreaseRate * Time.deltaTime * moveSpeed;
        }
    }
}
