using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levelPrefabs; // Array to hold different level prefabs
    public Transform player; // Reference to the player
    public float levelWidth = 20f; // Width of each level prefab
    public float minXDistance = 10f; // Minimum X distance to generate a new level
    public float yMinBoundary = -5f; // Minimum Y boundary value
    public float yMaxBoundary = 5f; // Maximum Y boundary value
    public Transform obstacleParent; // Parent transform for obstacles

    private List<GameObject> activeLevels = new List<GameObject>();
    private float nextLevelXPosition = 0f;

    void Start()
    {
        // Initialize the first level
        GenerateNewLevel();
    }

    void Update()
    {
        // Check if a new level should be generated
        if (player.position.x > nextLevelXPosition - minXDistance)
        {
            GenerateNewLevel();
        }
    }

    void GenerateNewLevel()
    {
        // Randomly select a level prefab
        GameObject levelPrefab = levelPrefabs[Random.Range(0, levelPrefabs.Length)];

        // Instantiate the selected level prefab
        GameObject newLevel = Instantiate(levelPrefab, new Vector3(nextLevelXPosition, 0, 0), Quaternion.identity, obstacleParent);

        // Add the new level to the list of active levels
        activeLevels.Add(newLevel);

        // Update the position for the next level
        nextLevelXPosition += levelWidth;

        // Clean up old levels
        if (activeLevels.Count > 4) // Arbitrary number to keep a few levels active
        {
            Destroy(activeLevels[0]);
            activeLevels.RemoveAt(0);
        }
    }

    public void ResetObstacles()
    {
        // Destroy all active levels
        foreach (GameObject level in activeLevels)
        {
            Destroy(level);
        }
        activeLevels.Clear();

        // Reset position for the next level
        nextLevelXPosition = 0f;

        // Generate the initial level again
        GenerateNewLevel();
    }
}
