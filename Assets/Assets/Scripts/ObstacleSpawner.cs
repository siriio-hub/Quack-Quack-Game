using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] obstacles;

    void Start()
    {
        SpawnObstacles();
    }

    public void SpawnObstacles()
    {
        foreach (Transform point in spawnPoints)
        {
            if (Random.value > 0.5f)
            {
                int rand = Random.Range(0, obstacles.Length);
                Instantiate(obstacles[rand], point.position, Quaternion.identity, transform);
            }
        }
    }
}
