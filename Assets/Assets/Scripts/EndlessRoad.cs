using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRoad : MonoBehaviour
{
    public GameObject[] segmentPrefabs;
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

        SpawnItems(segment);

        activeSegments.Add(segment);
        spawnZ += segmentLength;
    }

    void RecycleSegment()
    {
        GameObject oldSegment = activeSegments[0];
        activeSegments.RemoveAt(0);

        oldSegment.transform.position = new Vector3(0, 0, spawnZ);

        ClearOldItems(oldSegment);
        SpawnItems(oldSegment);

        spawnZ += segmentLength;
        activeSegments.Add(oldSegment);
    }
    void ClearOldItems(GameObject segment)
    {
        foreach (Transform child in segment.transform)
        {
            if (child.CompareTag("Item"))
            {
                Destroy(child.gameObject);
            }
        }
    }

    void SpawnItems(GameObject segment)
    {
        if (items.Length == 0) return;

        int obstacleLayerMask = LayerMask.GetMask("Obstacle");

        float minX = 1.2f; 
        float maxX = 4.0f;

        float zStart = -12f;
        float zEnd = 12f;
        float yPos = 1f;

        int itemsPerSide = 2;
        int maxAttempts = 5;

        float checkRadius = 1.5f;

        for (int side = 0; side < 2; side++) 
        {
            for (int i = 0; i < itemsPerSide; i++)
            {
                Vector3 spawnPos = Vector3.zero;
                bool isPosFound = false;

                for (int attempt = 0; attempt < maxAttempts; attempt++)
                {
                    float randX;
                    if (side == 0)
                    {
                        randX = Random.Range(-maxX, -minX);
                    }
                    else
                    {
                        randX = Random.Range(minX, maxX);
                    }

                    float randZ = Random.Range(zStart, zEnd);
                    spawnPos = segment.transform.position + new Vector3(randX, yPos, randZ);

                    if (!Physics.CheckSphere(spawnPos, checkRadius, obstacleLayerMask))
                    {
                        isPosFound = true;
                        break;
                    }
                }

                if (isPosFound)
                {
                    int randItemIndex = Random.Range(0, items.Length);
                    GameObject itemPrefab = items[randItemIndex];
                    Instantiate(itemPrefab, spawnPos, itemPrefab.transform.rotation, segment.transform);
                }
            }
        }
    }
}