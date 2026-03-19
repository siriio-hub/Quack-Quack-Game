using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRoad : MonoBehaviour
{
    public GameObject[] segmentPrefabs;
    public Transform[] players;

    public int segmentsOnScreen = 5;
    public float segmentLength = 30f;

    private float spawnZ = 0;
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
            new Vector3(0, 0, spawnZ),
            Quaternion.identity
        );

        ObstacleSpawner spawner = segment.GetComponent<ObstacleSpawner>();
        if (spawner != null)
        {
            spawner.SpawnObstacles();
        }

        activeSegments.Add(segment);
        spawnZ += segmentLength;
    }

    void RecycleSegment()
    {
        GameObject oldSegment = activeSegments[0];
        activeSegments.RemoveAt(0);

        oldSegment.transform.position = new Vector3(0, 0, spawnZ);

        ObstacleSpawner spawner = oldSegment.GetComponent<ObstacleSpawner>();
        if (spawner != null)
        {
            spawner.SpawnObstacles();
        }

        spawnZ += segmentLength;

        activeSegments.Add(oldSegment);
    }
}