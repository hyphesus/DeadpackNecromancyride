using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject continueButton;
    public GameObject optionsButton;
    public GameObject quitButton;
    public GameObject canvas; // Reference to the entire Canvas
    public GameObject simulationPanel; // Reference to the simulation panel
    public GameObject deathMessagePanel; // Reference to the death message panel
    public Text deathMessageText; // Reference to the text component in the death message panel
    public Transform player; // Reference to the player object
    public CameraFollow cameraFollow; // Reference to the CameraFollow script
    public GameObject pauseButton; // Reference to the pause button
    public GameObject mainMenuCanvas; // Reference to the main menu canvas (canvas?)
    public ConstantMovement playerMovement; // Reference to the ConstantMovement script

    private bool isSimulationRunning = false;
    private Vector3 initialPlayerPosition;

    void Start()
    {
        // Set initial state
        playButton.SetActive(true);
        continueButton.SetActive(false);
        canvas.SetActive(true); // Ensure the canvas is active at the start
        simulationPanel.SetActive(false); // Ensure the simulation panel is inactive at the start
        pauseButton.SetActive(false); // Ensure the pause button is inactive at the start
        deathMessagePanel.SetActive(false); // Ensure the death message panel is inactive at the start

        // Add listener for the pause button
        if (pauseButton != null)
        {
            pauseButton.GetComponent<Button>().onClick.AddListener(OnPauseButtonClick);
            Debug.Log("Pause button listener added");
        }
        else
        {
            Debug.LogError("Pause button is not assigned in the inspector");
        }

        Debug.Log("UIManager Start: Canvas active, SimulationPanel inactive");

        // Save the initial player position
        if (player != null)
        {
            initialPlayerPosition = player.position;
            Debug.Log("UIManager Start: initialPlayerPosition set to " + initialPlayerPosition);
        }
    }

    void Update()
    {
        // Debug to check if Update method is running
        Debug.Log("UIManager Update");

        // Check if ESC key is detected
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC key detected");
        }

        // Check if simulation is running and ESC key is pressed
        if (isSimulationRunning && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC pressed, pausing simulation");
            PauseSimulation();
        }
    }

    public void StartSimulation()
    {
        isSimulationRunning = true;
        canvas.SetActive(false); // Deactivate the entire canvas
        simulationPanel.SetActive(true); // Activate the simulation panel
        pauseButton.SetActive(true); // Activate the pause button
        cameraFollow.target = player; // Assign the player as the target for the camera
        mainMenuCanvas.SetActive(false); // Deactivate the main menu canvas

        // Start the player's movement
        playerMovement.StartMovement();

        // Add any additional logic to start the simulation
        Debug.Log("Simulation started: Canvas inactive, SimulationPanel active, isSimulationRunning set to true");
    }

    public void ContinueSimulation()
    {
        isSimulationRunning = true;
        canvas.SetActive(false); // Deactivate the entire canvas
        simulationPanel.SetActive(true); // Activate the simulation panel
        pauseButton.SetActive(true); // Activate the pause button
        cameraFollow.target = player; // Assign the player as the target for the camera

        // Continue the player's movement
        playerMovement.StartMovement();

        // Add any additional logic to continue the simulation
        Debug.Log("Simulation continued: Canvas inactive, SimulationPanel active, isSimulationRunning set to true");
    }

    public void PauseSimulation()
    {
        isSimulationRunning = false;
        canvas.SetActive(true); // Activate the entire canvas
        simulationPanel.SetActive(false); // Deactivate the simulation panel
        pauseButton.SetActive(false); // Deactivate the pause button
        playButton.SetActive(false);
        continueButton.SetActive(true);

        // Pause the player's movement
        playerMovement.StopMovement();

        // Add any additional logic to pause the simulation
        Debug.Log("Simulation paused: Canvas active, SimulationPanel inactive, isSimulationRunning set to false");
    }

    public void QuitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }

    public void ShowDeathMessage()
    {
        // Deactivate the main menu canvas
        mainMenuCanvas.SetActive(false);
        deathMessagePanel.SetActive(true); // Activate the death message panel
    }

    public void RestartSimulation()
    {
        // Ensure the player and simulationPanel are active before resetting
        if (player != null && simulationPanel != null && playerMovement != null)
        {
            player.gameObject.SetActive(true);
            simulationPanel.SetActive(true);
            mainMenuCanvas.SetActive(false); // Deactivate the main menu canvas

            // Reset the player position
            player.position = initialPlayerPosition;
            Debug.Log("UIManager RestartSimulation: Player position reset to " + initialPlayerPosition);

            // Reset the player's move speed
            playerMovement.ResetMoveSpeed();
            Debug.Log("UIManager RestartSimulation: Player moveSpeed reset to initialSpeed");

            playerMovement.StartMovement(); // Ensure movement starts
            Debug.Log("UIManager RestartSimulation: Player movement started");

            // Deactivate death message panel and activate necessary UI elements
            deathMessagePanel.SetActive(false);
            StartSimulation(); // Restart the simulation without showing the main menu
        }
        else
        {
            Debug.LogError("Player, SimulationPanel, or PlayerMovement is not assigned in the inspector");
        }
    }

    private void OnPauseButtonClick()
    {
        Debug.Log("Pause button clicked");
        if (isSimulationRunning)
        {
            Debug.Log("Pause button clicked, pausing simulation");
            PauseSimulation();
        }
    }
}
