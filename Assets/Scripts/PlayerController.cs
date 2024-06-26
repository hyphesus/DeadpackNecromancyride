using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    private bool isGrounded = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Ensure Rigidbody2D uses continuous collision detection
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Update()
    {
        // Update sameJumpSpeed with the current vertical velocity
        float sameJumpSpeed = rb.velocity.y;
        animator.SetFloat("sameJumpSpeed", sameJumpSpeed);

        // Debugging to see the current vertical speed
        Debug.Log("PlayerController: sameJumpSpeed: " + sameJumpSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
            animator.SetFloat("sameJumpSpeed", 0f);

            // Debugging collision with Terrain
            Debug.Log("Collision with Terrain: Player is grounded");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);

            // Debugging leaving collision with Terrain
            Debug.Log("Leaving collision with Terrain: Player is not grounded");
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
