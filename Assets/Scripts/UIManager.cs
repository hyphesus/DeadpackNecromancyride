using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject continueButton;
    public GameObject optionsButton;
    public GameObject quitButton;
    public GameObject canvas; // Reference to the entire Canvas
    public GameObject simulationPanel; // Reference to the simulation panel
    public Transform player; // Reference to the player object
    public CameraFollow cameraFollow; // Reference to the CameraFollow script

    private bool isSimulationRunning = false;

    void Start()
    {
        // Set initial state
        playButton.SetActive(true);
        continueButton.SetActive(false);
        canvas.SetActive(true); // Ensure the canvas is active at the start
        simulationPanel.SetActive(false); // Ensure the simulation panel is inactive at the start

        Debug.Log("UIManager Start: Canvas active, SimulationPanel inactive");
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
        cameraFollow.target = player; // Assign the player as the target for the camera
        // Add any additional logic to start the simulation
        Debug.Log("Simulation started: Canvas inactive, SimulationPanel active, isSimulationRunning set to true");
    }

    public void ContinueSimulation()
    {
        isSimulationRunning = true;
        canvas.SetActive(false); // Deactivate the entire canvas
        simulationPanel.SetActive(true); // Activate the simulation panel
        cameraFollow.target = player; // Assign the player as the target for the camera
        // Add any additional logic to continue the simulation
        Debug.Log("Simulation continued: Canvas inactive, SimulationPanel active, isSimulationRunning set to true");
    }

    public void PauseSimulation()
    {
        isSimulationRunning = false;
        canvas.SetActive(true); // Activate the entire canvas
        simulationPanel.SetActive(false); // Deactivate the simulation panel
        playButton.SetActive(false);
        continueButton.SetActive(true);
        // Add any additional logic to pause the simulation
        Debug.Log("Simulation paused: Canvas active, SimulationPanel inactive, isSimulationRunning set to false");
    }

    public void QuitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }
}
