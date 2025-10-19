using UnityEngine;
using System.Collections;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject cratePrefab;
    public GameObject propPrefab;

    public Transform player;

    public float laneOffset = 10f;          // lanes on Z-axis

    public float moveSpeed = 10f;

    public float minInterval = 1f;
    public float maxInterval = 2f;

    private float spawnTimer = 0f;
    private float nextSpawnTime = 0f;

    void Start()
    {
        nextSpawnTime = Random.Range(minInterval, maxInterval);
    }

    void Update()
    {
        if (player == null) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= nextSpawnTime)
        {
            SpawnObstacle();

            spawnTimer = 0f;
            nextSpawnTime = Random.Range(minInterval, maxInterval);
        }
    }

    void SpawnObstacle()
    {
        float zPos = Random.value < 0.5f ? -laneOffset : laneOffset;
        float xPos = player.position.z + 10;
        float op = Random.value < 0.5f ? -15 : 15;
        Vector3 spawnPos = new Vector3(op, 0f, zPos);

        GameObject prefab = Random.value < 0.5f ? cratePrefab : propPrefab;
        if (prefab == null) return;
        GameObject obstacle = Instantiate(prefab, spawnPos, Quaternion.identity);

    }

}
