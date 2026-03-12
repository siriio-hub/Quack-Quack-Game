using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRoad : MonoBehaviour
{
    public GameObject[] segmentPrefabs;
    public Transform player;

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
        if (player.position.z - 50 > spawnZ - (segmentsOnScreen * segmentLength))
        {
            RecycleSegment();
        }
    }

    void SpawnSegment()
    {
        int index = Random.Range(0, segmentPrefabs.Length);

        GameObject segment = Instantiate(
            segmentPrefabs[index],
            new Vector3(0, 0, spawnZ),
            Quaternion.identity
        );

        activeSegments.Add(segment);

        spawnZ += segmentLength;
    }

    void RecycleSegment()
    {
        GameObject oldSegment = activeSegments[0];
        activeSegments.RemoveAt(0);

        int index = Random.Range(0, segmentPrefabs.Length);

        oldSegment.transform.position = new Vector3(0, 0, spawnZ);

        spawnZ += segmentLength;

        activeSegments.Add(oldSegment);
    }
}