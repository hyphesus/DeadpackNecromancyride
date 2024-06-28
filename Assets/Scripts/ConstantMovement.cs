using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    public float initialSpeed = 2f; // Initial speed at which the object starts moving
    public float speedIncreaseRate = 0.1f; // Rate at which the speed increases over time
    private float moveSpeed;
    private bool isMoving = true;

    void Start()
    {
        moveSpeed = initialSpeed;
        Debug.Log("ConstantMovement Start: initialSpeed set to " + initialSpeed + ", moveSpeed set to " + moveSpeed);
    }

    void Update()
    {
        if (isMoving)
        {
            // Move the object to the right with a linear increasing speed
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            Debug.Log("ConstantMovement Update: Moving to the right with speed " + moveSpeed);

            // Increase the speed linearly
            moveSpeed += speedIncreaseRate * Time.deltaTime;
            Debug.Log("ConstantMovement Update: Speed increased to " + moveSpeed);
        }
        else
        {
            Debug.Log("ConstantMovement Update: isMoving is false");
        }
    }

    public void ResetMoveSpeed()
    {
        moveSpeed = initialSpeed;
        Debug.Log("ConstantMovement ResetMoveSpeed: moveSpeed reset to " + initialSpeed);
    }

    public void StartMovement()
    {
        isMoving = true;
        Debug.Log("ConstantMovement: Movement started.");
    }

    public void StopMovement()
    {
        isMoving = false;
        Debug.Log("ConstantMovement: Movement stopped.");
    }
}
