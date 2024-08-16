using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCarsSpawnScript : MonoBehaviour
{
    [SerializeField] private Transform[] carsList;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnDelay = 2f;

    private float spawnDelayMax = 0;

    private void Update()
    {
        spawnDelayMax -= Time.deltaTime;
        if(spawnDelayMax <= 0)
        {
            spawnDelayMax = spawnDelay;
            SpawnCar();
        }
    }
    private void SpawnCar()
    {
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Transform car = carsList[Random.Range(0, carsList.Length)];

        Instantiate(car, spawn.position, car.rotation);
    }
}
