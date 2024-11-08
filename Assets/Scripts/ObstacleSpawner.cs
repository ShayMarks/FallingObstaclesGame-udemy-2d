using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;

    private float spawnInterval = 1.0f; // Starting time between spawns
    private float initialSpawnInterval = 1.0f;

    private float timeSinceLastSpawn = 0f;

    private float screenHalfWidthInWorldUnits;

    private float difficultyIncreaseInterval = 5f; // every 5 seconds difficulty increases
    private float timeSinceDifficultyIncrease = 0f;

    public float initialFallSpeed = 5f;

    void Start()
    {
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
        // Initialize fall speeds
        Obstacle.fallSpeedGlobal = initialFallSpeed;
        Coin.fallSpeedGlobal = initialFallSpeed;
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        timeSinceDifficultyIncrease += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnObstacleOrCoin();
            timeSinceLastSpawn = 0f;
        }

        if (timeSinceDifficultyIncrease >= difficultyIncreaseInterval)
        {
            IncreaseDifficulty();
            timeSinceDifficultyIncrease = 0f;
        }
    }

    void SpawnObstacleOrCoin()
    {
        // Determine whether to spawn obstacle or coin
        float spawnChance = Random.Range(0f, 1f);

        GameObject prefabToSpawn;

        if (spawnChance < 0.7f) // 70% chance to spawn obstacle
        {
            prefabToSpawn = obstaclePrefab;
        }
        else // 30% chance to spawn coin
        {
            prefabToSpawn = coinPrefab;
        }

        float xPosition = Random.Range(-screenHalfWidthInWorldUnits + 0.5f, screenHalfWidthInWorldUnits - 0.5f);
        Vector3 spawnPosition = new Vector3(xPosition, Camera.main.transform.position.y + Camera.main.orthographicSize + 1f, 0f);

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

    void IncreaseDifficulty()
    {
        // Decrease spawn interval to spawn more frequently
        if (spawnInterval > 0.3f)
        {
            spawnInterval -= 0.1f;
        }

        // Increase fall speed
        Obstacle.fallSpeedGlobal += 1f; // increase by 1f every 5 seconds
        Coin.fallSpeedGlobal += 1f;
    }

    public void ResetSpawner()
    {
        spawnInterval = initialSpawnInterval;
        timeSinceLastSpawn = 0f;
        timeSinceDifficultyIncrease = 0f;

        // Reset fall speeds
        Obstacle.fallSpeedGlobal = initialFallSpeed;
        Coin.fallSpeedGlobal = initialFallSpeed;

        Debug.Log("ObstacleSpawner has been reset.");
    }
}
