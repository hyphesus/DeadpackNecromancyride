using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private UIManager uiManager; // Reference to the UIManager

    private bool isGrounded = false;
    private float groundedTimer = 0.1f; // Time to wait before setting grounded state
    private float groundedCounter = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        uiManager = FindObjectOfType<UIManager>(); // Find the UIManager in the scene
    }

    void Update()
    {
        // Update the grounded counter
        if (groundedCounter > 0)
        {
            groundedCounter -= Time.deltaTime;
            if (groundedCounter <= 0)
            {
                isGrounded = true;
                animator.SetBool("isGrounded", true);
            }
        }

        // Update sameJumpSpeed with the current vertical velocity
        float verticalSpeed = rb.velocity.y;
        animator.SetFloat("sameJumpSpeed", verticalSpeed);

        // Debugging to see the current vertical speed
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
            groundedCounter = groundedTimer; // Start the grounded counter
            isGrounded = true;
            animator.SetBool("isGrounded", true);
            animator.SetFloat("sameJumpSpeed", 0f);
            Debug.Log("Collision with Terrain: Player is grounded");
        }
        else if (collision.gameObject.CompareTag("Dangerous"))
        {
            Debug.Log("Collision with Dangerous object");
            animator.SetTrigger("DieTrigger");
            StartCoroutine(HandleDeath());
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

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(2f); // Wait for 1 second before showing the death message
        uiManager.PauseSimulation();
        uiManager.ShowDeathMessage();
    }
}
