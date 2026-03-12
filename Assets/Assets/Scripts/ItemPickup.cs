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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            switch (itemType)
            {
                case ItemType.Heart:
                    player.AddHealth(1);
                    break;

                case ItemType.Coin:
                    GameManager.instance.AddScore(1, player.playerID);
                    break;

                case ItemType.GoldenEgg:
                    GameManager.instance.AddScore(5, player.playerID);
                    break;

                case ItemType.SpeedBoost:
                    player.SpeedBoost();
                    break;

                case ItemType.Shield:
                    player.ActivateShield();
                    break;
            }

            // Item หายเฉพาะตอนเก็บ
            Destroy(gameObject);
        }
    }
}