using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> obstaclesList;
    [SerializeField] private float spawnDelay;

    private float spawnDelayMax = 0;
    private Vector3[] corners = new Vector3[4];
    private DifficultyRiser difficulty;
    private void Awake()
    {
        RectTransform rt = GameObject.Find("Canvas").GetComponent<RectTransform>();

        rt.GetWorldCorners(corners);
    }
    private void Start()
    {
        difficulty = DifficultyRiser.Instance;

        PlayerController.Instance.OnDied += StopSpawning;
    }

    private void Update()
    {
        spawnDelayMax -= Time.deltaTime;
        if (spawnDelayMax <= 0)
        {
            float currentDelay = spawnDelay / difficulty.GetCurrentMultiplier();
            spawnDelayMax = currentDelay;
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        Transform obstacle = obstaclesList[Random.Range(0, obstaclesList.Count)];
        Vector3 spawnPos = new Vector3(Random.Range(corners[1].x, corners[2].x), 0, 35);

        Instantiate(obstacle, spawnPos, obstacle.rotation);
    }

    private void StopSpawning()
    {
        Destroy(this);
    }
}
