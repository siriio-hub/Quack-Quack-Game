using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    // SerializeField show in inspector
    [Space]
    [SerializeField] private Vector3 offset = new Vector3(0, 9f, -10f);

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        transform.rotation = Quaternion.Euler(10f, 0f, 0f);
    }
}
