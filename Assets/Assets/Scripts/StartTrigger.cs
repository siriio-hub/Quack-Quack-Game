using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    public GameObject startRoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(startRoad, 5f);
        }
    }
}