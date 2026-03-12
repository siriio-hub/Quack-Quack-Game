using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float forwardSpeed = 8f;
    public float sideSpeed = 10f;
    public float jumpForce = 10f;

    public string inputID;

    private int jumpCount = 0;
    public int maxJump = 2;

    [Header("Key Setting")]
    public KeyCode jumpKey;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ¡ÃÐâ´´
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
    }
}