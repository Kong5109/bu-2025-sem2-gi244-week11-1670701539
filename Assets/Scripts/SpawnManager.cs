using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public float startSpawnDelay = 5f;
    public float spawnDelay = 3f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        //InvokeRepeating(nameof(RandomSpawn), 0, 5);
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(startSpawnDelay);
        while (true)
        {
            RandomSpawn();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void RandomSpawn()
    {
        var index = Random.Range(0, spawnPoints.Length);
        var spawnPos = spawnPoints[index];
        Instantiate(enemyPrefab, spawnPos.position, Quaternion.identity);
    }
}