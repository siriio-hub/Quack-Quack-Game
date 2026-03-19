using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    public GameObject[] items;

    public Transform player;

    public int spawnCount = 3;
    public float xRange = 4f;
    public float zRange = 20f;
    public float yPos = 1f;

    float lastSpawnZ = 0f;

    void Update()
    {
        if (player.position.z > lastSpawnZ - zRange)
        {
            SpawnItems();
            lastSpawnZ += zRange;
        }
    }

    void SpawnItems()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            float randX = Random.Range(-xRange, xRange);
            float randZ = Random.Range(0, zRange);

            Vector3 spawnPos = new Vector3(randX, yPos, lastSpawnZ + randZ);

            int randItem = Random.Range(0, items.Length);

            Instantiate(items[randItem], spawnPos, Quaternion.identity);
        }
    }
}