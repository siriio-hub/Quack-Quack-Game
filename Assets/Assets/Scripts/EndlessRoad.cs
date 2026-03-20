using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRoad : MonoBehaviour
{
    public GameObject[] segmentPrefabs;
    public GameObject[] obstaclePrefabs;
    public GameObject[] items;
    public Transform[] players;

    public int segmentsOnScreen = 5;
    public float segmentLength = 30f;

    private float spawnZ = 10;
    private List<GameObject> activeSegments = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < segmentsOnScreen; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        float backPlayerZ = GetBackPlayerZ();

        float safeZone = spawnZ - (segmentsOnScreen * segmentLength);

        if (backPlayerZ > safeZone + 50f)
        {
            RecycleSegment();
        }
    }

    float GetBackPlayerZ()
    {
        float minZ = players[0].position.z;

        for (int i = 1; i < players.Length; i++)
        {
            if (players[i].position.z < minZ)
            {
                minZ = players[i].position.z;
            }
        }

        return minZ;
    }

    void SpawnSegment()
    {
        int index = Random.Range(0, segmentPrefabs.Length);

        GameObject segment = Instantiate(
            segmentPrefabs[index],
            new Vector3(0, 0, spawnZ + (segmentLength / 2)),
            Quaternion.identity
        );

        Obstacles(segment);

        activeSegments.Add(segment);
        spawnZ += segmentLength;
    }

    void RecycleSegment()
    {
        GameObject oldSegment = activeSegments[0];
        activeSegments.RemoveAt(0);

        oldSegment.transform.position = new Vector3(0, 0, spawnZ);

        Obstacles(oldSegment);

        spawnZ += segmentLength;
        activeSegments.Add(oldSegment);
    }
    void Obstacles(GameObject segment)
    {
        Vector3[] obstaclePositions = new Vector3[]
        {
            new Vector3(-2f, 0.5f, 5f),
            new Vector3(0f, 0.5f, 10f),
            new Vector3(2f, 0.5f, 15f)
        };

        foreach (Transform child in segment.transform)
        {
            if (child.CompareTag("Obstacle"))
                Destroy(child.gameObject);
        }

        for (int i = 0; i < obstaclePrefabs.Length && i < obstaclePositions.Length; i++)
        {
            Instantiate(obstaclePrefabs[i], segment.transform.position + obstaclePositions[i], Quaternion.identity, segment.transform);
        }

        SpawnItems(segment);
    }
    void SpawnItems(GameObject segment)
    {
        float xRange = 4f;
        float zStart = 5f;
        float zEnd = 25f; 
        float yPos = 1f;

        int spawnCount = 3; 

        for (int i = 0; i < spawnCount; i++)
        {
            float randX = Random.Range(-xRange, xRange);
            float randZ = Random.Range(zStart, zEnd);

            Vector3 spawnPos = segment.transform.position + new Vector3(randX, yPos, randZ);

            int randItem = Random.Range(0, obstaclePrefabs.Length);
            GameObject itemPrefab = items[randItem];

            Instantiate(itemPrefab, spawnPos, Quaternion.identity, segment.transform);
        }
    }
}