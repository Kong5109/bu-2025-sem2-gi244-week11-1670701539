using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] powerUpPrefabs;

    private int currentWaveIndex = 0;
    private bool isFinishSpawnEnemy = false;
    private void Start()
    {
        StartCoroutine(ProcessWaveRoutine());
    }

    private IEnumerator ProcessWaveRoutine()
    {
        while (currentWaveIndex < waves.Length)
        {
            Debug.Log("Start WaveSpawner at WaveIndex " + currentWaveIndex);

            Wave wave = waves[currentWaveIndex];

            List<Transform> selectPoints = GetRandomSpawnPoints(wave.numberOfRandomSpawnPoint);
            SpawnPowerUps(wave.numberOfPowerUp, selectPoints);

            yield return new WaitForSeconds(wave.delayStart);

            StartCoroutine(SpawnEnemies(wave, selectPoints));

            while (isFinishSpawnEnemy == false)
            {
                yield return null;
            }

            Debug.Log("End WaveSpawner at WaveIndex " + currentWaveIndex);

            currentWaveIndex++;
        }
        Debug.Log("WaveSpawner End Process");
    }

    private IEnumerator SpawnEnemies(Wave wave, List<Transform> spawnPoints)
    {
        isFinishSpawnEnemy = false;
        for (int i = 0; i < wave.totalSpawnEnemies; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Count);
            Transform point = spawnPoints[spawnPointIndex];

            GameObject enemy = Instantiate(enemyPrefab, point.position, Quaternion.identity);

            yield return new WaitForSeconds(wave.spawnInterval);
        }
        isFinishSpawnEnemy = true;
    }

    #region WaveStartSetUp
    private void SpawnPowerUps(int amount, List<Transform> points)
    {
        for (int i = 0; i < amount; i++)
        {
            int spawnPointIndex = Random.Range(0, points.Count);
            int powerIndex = Random.Range(0, powerUpPrefabs.Length);
               
            Transform spawnPoint = points[spawnPointIndex];
            GameObject powerUp = powerUpPrefabs[powerIndex];

            Instantiate(powerUp, spawnPoint.position, Quaternion.identity);
        }
    }

    private List<Transform> GetRandomSpawnPoints(int count)
    {
        List<Transform> possiblePoint = new List<Transform>();
        foreach (Transform point in spawnPoints)
        {
            possiblePoint.Add(point);
        }
        List<Transform> result = new List<Transform>();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, possiblePoint.Count);

            result.Add(possiblePoint[index]);
            possiblePoint.RemoveAt(index);
        }

        return result;
    }
    #endregion
}
