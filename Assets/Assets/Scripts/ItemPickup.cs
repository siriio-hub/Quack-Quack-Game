using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        Heart,
        Coin,
        GoldenEgg,
        SpeedBoost,
        Shield
    }

    public ItemType itemType;
    public GameObject pickupVFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player == null) return;

            if (GameManager.instance == null)
            {
                Debug.LogWarning("ลืมวาง GameManager ไว้ในฉากหรือเปล่า?");
                return;
            }

            if (pickupVFX != null)
            {
                GameObject vfx = Instantiate(pickupVFX, transform.position, Quaternion.identity);
                Destroy(vfx, 2f);
            }

            switch (itemType)
            {
                case ItemType.Heart:
                    GameManager.instance.AddHealth(1, player.playerID);
                    break;

                case ItemType.Coin:
                    GameManager.instance.AddScore(1, player.playerID);
                    break;

                case ItemType.GoldenEgg:
                    GameManager.instance.AddScore(5, player.playerID);
                    int randomAttack = Random.Range(0, 2);
                    if (randomAttack == 0) player.LaunchTornado();
                    else player.LaunchBubble();
                    break;

                case ItemType.SpeedBoost:
                    player.SpeedBoost();
                    break;

                case ItemType.Shield:
                    player.ActivateShield();
                    break;
            }

            Destroy(gameObject);
        }
    }
}