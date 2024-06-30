using UnityEngine;

public class Jump : MonoBehaviour
{
    public float initialJumpSpeed = 1f; // Initial speed of the jump
    public float speedIncreaseRate = 0.1f; // Rate at which the speed increases each millisecond
    public float maxJumpSpeed = 10f; // Maximum speed of the jump
    public float sameJumpSpeed = 0f; // Public float to track the jump speed

    public AudioClip jumpSound; // Assign this in the Inspector
    private AudioSource audioSource;

    private bool isJumping = false;
    private float currentJumpSpeed;
    private float timeSinceJumpStart;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            // If the AudioSource component is not attached to the player, add it
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Check for input to start jumping
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump") || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            StartJump();

            // Play jump sound
            if (jumpSound != null)
            {
                audioSource.volume = 0.1f;
                audioSource.PlayOneShot(jumpSound);
            }
        }

        // Check for input release to stop jumping
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Jump") || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            StopJump();
        }

        // Update sameJumpSpeed with the current vertical velocity when not jumping
        if (!isJumping)
        {
            sameJumpSpeed = rb.velocity.y;
        }

        // If the object is jumping, update its position
        if (isJumping)
        {
            PerformJump();
        }

        // Debugging to see the current jump/fall speed
        Debug.Log("sameJumpSpeed: " + sameJumpSpeed);
    }

    void StartJump()
    {
        if (!isJumping)
        {
            isJumping = true;
            currentJumpSpeed = initialJumpSpeed;
            timeSinceJumpStart = 0f;
            rb.velocity = new Vector2(rb.velocity.x, currentJumpSpeed);
        }
    }

    void StopJump()
    {
        isJumping = false;
    }

    void PerformJump()
    {
        // Update the jump speed based on the time since the jump started
        timeSinceJumpStart += Time.deltaTime;
        currentJumpSpeed += speedIncreaseRate * Time.deltaTime * 1000; // Multiply by 1000 to convert seconds to milliseconds

        // Cap the jump speed to maxJumpSpeed
        if (currentJumpSpeed > maxJumpSpeed)
        {
            currentJumpSpeed = maxJumpSpeed;
        }

        // Apply the vertical force to the Rigidbody2D
        rb.velocity = new Vector2(rb.velocity.x, currentJumpSpeed);

        // Set sameJumpSpeed to the current jump speed
        sameJumpSpeed = currentJumpSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            StopJump();
            currentJumpSpeed = 0f;
            sameJumpSpeed = rb.velocity.y; // Update sameJumpSpeed to the current falling speed
        }
    }
}
