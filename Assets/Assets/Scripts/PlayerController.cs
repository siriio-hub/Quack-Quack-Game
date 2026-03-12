using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float forwardSpeed = 8f;
    public float sideSpeed = 10f;
    public float jumpForce = 10f;
    public bool isShieldActive = false;

    float normalSpeed;

    public int health = 3;

    public string inputID;

    private int jumpCount = 0;
    public int maxJump = 2;

    [Header("Key Setting")]
    public KeyCode jumpKey;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        normalSpeed = forwardSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(jumpKey) && jumpCount < maxJump)
        {
            playerRb.linearVelocity = new Vector3(
                playerRb.linearVelocity.x,
                0,
                playerRb.linearVelocity.z
            );

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal" + inputID);

        Vector3 velocity = new Vector3(
            moveX * sideSpeed,
            playerRb.linearVelocity.y,
            forwardSpeed
        );

        playerRb.linearVelocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!isShieldActive)
            {
                health--;
                Debug.Log("Hit! Health: " + health);
            }
            else
            {
                Debug.Log("Shield Blocked Damage!");
            }
        }
    }

    public void SpeedBoost()
    {
        forwardSpeed += 5f;
        Debug.Log("Speed Boost!");
        Invoke("ResetSpeed", 5f);
    }

    void ResetSpeed()
    {
        forwardSpeed = normalSpeed;
    }

    public void AddHealth(int amount)
    {
        health += amount;
        Debug.Log("Health: " + health);
    }
    public void ActivateShield()
    {
        isShieldActive = true;
        Debug.Log("Shield Activated!");
        Invoke("DeactivateShield", 10f);
    }

    void DeactivateShield()
    {
        isShieldActive = false;
        Debug.Log("Shield Ended");
    }
}