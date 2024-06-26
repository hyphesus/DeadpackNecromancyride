using UnityEngine;

public class ConstantVelocity : MonoBehaviour
{
    public float constantSpeed = -5f; // The constant downward speed

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity effect
    }

    void FixedUpdate()
    {
        // Set the velocity directly to a constant value
        rb.velocity = new Vector2(rb.velocity.x, constantSpeed);
    }
}
