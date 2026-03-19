using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] obstacles;
    public Transform obstacleParent;

    void Start()
    {
        SpawnObstacles();
    }

    public void SpawnObstacles()
    {
        foreach (Transform child in obstacleParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform point in spawnPoints)
        {
            if (Random.value > 0.5f && obstacles.Length > 0)
            {
                int rand = Random.Range(0, obstacles.Length);

                Instantiate(
                    obstacles[rand],
                    point.position,
                    point.rotation,
                    transform
                );
            }
        }
    }
}