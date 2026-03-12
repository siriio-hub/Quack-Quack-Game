using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject startSegment;
    public GameObject finishSegment;
    public GameObject[] roadSegments;

    public Transform player;

    public float segmentLength = 30f;
    public int segmentsBeforeFinish = 10;

    private float spawnZ = 0;
    private int segmentsSpawned = 0;

    void Start()
    {
        SpawnSegment(startSegment);

        for (int i = 0; i < 3; i++)
        {
            SpawnRandomSegment();
        }
    }

    void Update()
    {
        if (player.position.z + 60 > spawnZ)
        {
            if (segmentsSpawned >= segmentsBeforeFinish)
            {
                SpawnSegment(finishSegment);
                enabled = false;
            }
            else
            {
                SpawnRandomSegment();
            }
        }
    }

    void SpawnRandomSegment()
    {
        int id = Random.Range(0, roadSegments.Length);
        SpawnSegment(roadSegments[id]);
        segmentsSpawned++;
    }

    void SpawnSegment(GameObject segment)
    {
        Instantiate(segment, Vector3.forward * spawnZ, Quaternion.identity);
        spawnZ += segmentLength;
    }
}