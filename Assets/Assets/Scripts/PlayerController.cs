using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator animator;

    public int playerID;
    private string horizontalAxis;

    [Header("Key Setting")]
    public KeyCode jumpKey;

    private float normalSpeed;
    public float forwardSpeed = 8f;
    public float sideSpeed = 10f;
    public float jumpForce = 14f;
    private int jumpCount = 0;
    public int maxJump = 2;

    public int health = 3;
    public int maxHealth = 3;

    public bool isShieldActive = false;

    private bool isKnockback = false;
    private float hitCooldown = 0.5f;
    private float lastHitTime = -999f;

    private bool isDead = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        horizontalAxis = "Horizontal" + playerID;

        normalSpeed = forwardSpeed;
    }

    void Update()
    {
        if (isDead)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        float moveX = Input.GetAxis(horizontalAxis);
        float speed = Mathf.Abs(moveX) + Mathf.Abs(forwardSpeed);
        animator.SetFloat("Speed", speed);

        if (Input.GetKeyDown(jumpKey) && jumpCount < maxJump)
        {
            playerRb.linearVelocity = new Vector3(
                playerRb.linearVelocity.x,
                0,
                playerRb.linearVelocity.z
            );

            playerRb.AddForce(Vector3.up * jumpForce * 1.2f, ForceMode.Impulse);
            jumpCount++;

            animator.ResetTrigger("Jump");
            animator.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        if (isDead || isKnockback)
        {
            playerRb.linearVelocity = new Vector3(0, playerRb.linearVelocity.y, 0);
            return;
        }

        float moveX = Input.GetAxis(horizontalAxis);

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

        //if (collision.gameObject.CompareTag("Obstacle"))
        //{
        //    if (!isShieldActive)
        //    {
        //        health--;
        //        Debug.Log("Hit! Health: " + health);
        //    }
        //    else
        //    {
        //        Debug.Log("Shield Blocked Damage!");
        //    }
        //}
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && Time.time > lastHitTime + hitCooldown)
        {
            lastHitTime = Time.time;

            TakeDamage();

            isKnockback = true;

            playerRb.linearVelocity = new Vector3(0, playerRb.linearVelocity.y, 0);
            
            transform.position += Vector3.back * 2f;

            Vector3 knockback = new Vector3(0, 2f, -4f);
            playerRb.AddForce(knockback, ForceMode.Impulse);

            Invoke("EndKnockback", 0.7f);
        }

        if (other.CompareTag("Heart"))
        {
            AddHealth(1);
            Destroy(other.gameObject);
        }
    }
    void TakeDamage()
    {
        if (isShieldActive) return;

        health--;
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    public void SpeedBoost()
    {
        forwardSpeed = normalSpeed + 5f;
        Invoke("ResetSpeed", 5f);
    }

    void ResetSpeed()
    {
        forwardSpeed = normalSpeed;
    }

    public void AddHealth(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
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

    void EndKnockback()
    {
        isKnockback = false;
        playerRb.linearVelocity = new Vector3(
            0,
            playerRb.linearVelocity.y,
            forwardSpeed
        );
    }

    void Die()
    {
        isDead = true;

        playerRb.linearVelocity = Vector3.zero;
        forwardSpeed = 0;

        animator.SetFloat("Speed", 0);
        animator.SetTrigger("Die");

        Invoke("GameOver", 2f);
    }
}