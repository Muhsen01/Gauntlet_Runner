using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Reference to prefabs and spawn intervals
    public GameObject powerupPrefab;
    public GameObject obstaclePrefab;
    public float spawnCycle = 0.5f;
    public float powerupSpawnCycle = 5f; // Interval for spawning power-ups
    public float obstacleSpawnCycle = 2f; // Interval for spawning obstacles

    // Timers and intervals for power-ups and obstacles
    float powerUpTimer = 0f;
    float obstacleTimer = 0f;
    float powerUpSpawnInterval = 5f; // Default interval for power-ups
    float obstacleSpawnInterval = 2f; // Default interval for obstacles

    GameManager manager; // Reference to the GameManager script
    float elapsedTime; // Elapsed time since the last spawn
    bool spawnPowerup = true; // Flag to alternate between spawning power-ups and obstacles

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<GameManager>(); // Access GameManager script from the current object
    }

    // Update is called once per frame
    void Update()
    {
        // Update power-up spawn timer
        powerUpTimer += Time.deltaTime;
        if (powerUpTimer >= powerUpSpawnInterval)
        {
            SpawnPowerUp();
            powerUpTimer = 0f; // Reset the timer
        }

        // Update obstacle spawn timer
        obstacleTimer += Time.deltaTime;
        if (obstacleTimer >= obstacleSpawnInterval)
        {
            SpawnObstacle();
            obstacleTimer = 0f; // Reset the timer
        }

        // Spawn objects based on spawn cycle
        elapsedTime += Time.deltaTime;
        if (elapsedTime > spawnCycle)
        {
            GameObject temp;
            if (spawnPowerup)
                temp = Instantiate(powerupPrefab) as GameObject;
            else
                temp = Instantiate(obstaclePrefab) as GameObject;

            Vector3 position = temp.transform.position;
            position.x = Random.Range(-3f, 3f);
            temp.transform.position = position;

            Collidable col = temp.GetComponent<Collidable>();
            col.manager = manager; // Ensure interaction with the GameManager

            elapsedTime = 0; // Reset the elapsed time
            spawnPowerup = !spawnPowerup; // Alternate between spawning power-ups and obstacles
        }
    }

    // Spawn a power-up GameObject
    void SpawnPowerUp()
    {
        GameObject newPowerUp = Instantiate(powerupPrefab) as GameObject;

        Vector3 position = newPowerUp.transform.position;
        position.x = Random.Range(-3f, 3f);
        newPowerUp.transform.position = position;

        Collidable col = newPowerUp.GetComponent<Collidable>();
        col.manager = manager; // Ensure interaction with the GameManager
    }

    // Spawn an obstacle GameObject
    void SpawnObstacle()
    {
        GameObject newObstacle = Instantiate(obstaclePrefab) as GameObject;

        Vector3 position = newObstacle.transform.position;
        position.x = Random.Range(-3f, 3f);
        newObstacle.transform.position = position;

        Collidable col = newObstacle.GetComponent<Collidable>();
        col.manager = manager; // Ensure interaction with the GameManager
    }
}
