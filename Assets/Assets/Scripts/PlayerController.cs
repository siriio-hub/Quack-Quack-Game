using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float jumpForce = 10f;
    public float speed = 8f;
    private int jumpCount = 0;
    public int maxJump = 2;

    [Header("Key Setting")]
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode forwardKey;
    public KeyCode backwardKey;
    public KeyCode jumpKey;

    [Header("Movement Limit")]
    public float minX = -10f;
    public float maxX = 10f;

    [Header("Speed Boost")]
    public float boostMultiplier = 1.5f;
    public float boostDuration = 10f;
    private float originalSpeed;
    private bool isBoosted = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        originalSpeed = speed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey) && jumpCount < maxJump)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(leftKey))
            move += Vector3.left;

        if (Input.GetKey(rightKey))
            move += Vector3.right;

        if (Input.GetKey(forwardKey))
            move += Vector3.forward;

        if (Input.GetKey(backwardKey))
            move += Vector3.back;

        playerRb.MovePosition(playerRb.position + move * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            StartCoroutine(SpeedBoost());
        }
    }
    IEnumerator SpeedBoost()
    {
        if (isBoosted) yield break;

        isBoosted = true;
        speed *= boostMultiplier;

        yield return new WaitForSeconds(boostDuration);

        speed = originalSpeed;
        isBoosted = false;
    }
}