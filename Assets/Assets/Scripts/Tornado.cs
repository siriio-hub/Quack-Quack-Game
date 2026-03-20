using UnityEngine;

public class Tornado : MonoBehaviour
{
    public float speed = 3f;
    public GameObject hitVFX;
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.TakeDamage();

                if (hitVFX != null)
                {
                    Instantiate(hitVFX, transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }
    }
}
