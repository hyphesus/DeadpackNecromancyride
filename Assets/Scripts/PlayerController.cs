using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private UIManager uiManager; // Reference to the UIManager
    private AudioSource audioSource;

    private bool isGrounded = false;
    private float groundedTimer = 0.1f; // Time to wait before setting grounded state
    private float groundedCounter = 0f;
    public bool isDying = false;
    public AudioClip deathSound;

    public Text scoreText; // Reference to the score text UI element
    public Text highScoreText; // Reference to the high score text UI element
    private float initialXPosition;
    private float score = 0f;
    private int highScore = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        uiManager = FindObjectOfType<UIManager>(); // Find the UIManager in the scene
        audioSource = GetComponent<AudioSource>();
        // Initialize the initial X position
        initialXPosition = transform.position.x;

        // Find the score text UI element
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        highScoreText.text = "High Score: " + highScore;
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

        float distanceTraveled = transform.position.x - initialXPosition;
        score = distanceTraveled * 120;
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
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
        else if (collision.gameObject.CompareTag("Dangerous") && !isDying)
        {
            isDying = true; // Set the flag to indicate the dying animation has been triggered
            Debug.Log("Collision with Dangerous object");
            animator.SetTrigger("DieTrigger");

            // Get the DieParticle child object's Animator and set the DieTrigger
            Animator dieParticleAnimator = transform.Find("DieParticle").GetComponent<Animator>();
            dieParticleAnimator.SetTrigger("DieTrigger");

            // Disable the Jump script
            Jump jumpScript = GetComponent<Jump>();
            if (jumpScript != null)
            {
                jumpScript.enabled = false;
            }

            if (deathSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(deathSound);
            }

            // Shake the camera
            StartCoroutine(ShakeCamera());

            // Check for new high score
            CheckForHighScore();

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
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before showing the death message
        uiManager.PauseSimulation();
        uiManager.ShowDeathMessage();
    }

    private IEnumerator ShakeCamera()
    {
        float duration = 0.5f; // Duration of the shake
        float magnitude = 0.1f; // Magnitude of the shake

        Vector3 originalPosition = Camera.main.transform.position;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.position = originalPosition;
    }

    public void SetIsDying()
    {
        isDying = false;
    }
    private void CheckForHighScore()
    {
        int currentScore = Mathf.FloorToInt(score);
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "High Score: " + highScore;
        }
    }
}
