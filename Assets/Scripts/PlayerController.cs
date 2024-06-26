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
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Update()
    {
        float verticalSpeed = rb.velocity.y;
        animator.SetFloat("sameJumpSpeed", verticalSpeed);

        Debug.Log("PlayerController: sameJumpSpeed: " + verticalSpeed);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public float GetVerticalSpeed()
    {
        return rb.velocity.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
            animator.SetFloat("sameJumpSpeed", 0f);
            Debug.Log("Collision with Terrain: Player is grounded");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
            Debug.Log("Leaving collision with Terrain: Player is not grounded");
        }
    }
}
