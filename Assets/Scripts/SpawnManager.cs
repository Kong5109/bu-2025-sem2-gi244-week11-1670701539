using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    void Start()
    {
        InvokeRepeating(nameof(RandomSpawn), 0, 5);
    }

    void RandomSpawn()
    {
        var index = Random.Range(0, spawnPoints.Length);
        var spawnPos = spawnPoints[index];
        Instantiate(enemyPrefab, spawnPos.position, Quaternion.identity);
    }
}
