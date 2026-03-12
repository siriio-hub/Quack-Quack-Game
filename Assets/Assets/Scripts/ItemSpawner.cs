using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    public GameObject[] items;

    public int spawnCount = 3;     // จำนวนไอเทมที่จะสุ่ม
    public float xRange = 4f;      // กว้างของแมพ
    public float zRange = 20f;     // ยาวของแมพ
    public float yPos = 1f;        // ความสูง

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            float randX = Random.Range(-xRange, xRange);
            float randZ = Random.Range(0, zRange);

            Vector3 spawnPos = new Vector3(randX, yPos, transform.position.z + randZ);

            int randItem = Random.Range(0, items.Length);

            Instantiate(items[randItem], spawnPos, Quaternion.identity, transform);
        }
    }
}