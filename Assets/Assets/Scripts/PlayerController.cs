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

    [Header("Shield Effect")]
    public bool isShieldActive = false;
    public GameObject shieldEffect;

    [Header("Speed Boost VFX")]
    public ParticleSystem dustVFX;
    public ParticleSystem lightningVFX;
    public ParticleSystem speedLinesVFX;

    [Header("Attack Settings")]
    public GameObject tornadoPrefab;

    public GameObject bubblePrefab;
    public bool isBubbleTrapped = false;

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

        if (shieldEffect != null) shieldEffect.SetActive(false);
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
            if (dustVFX != null) dustVFX.Stop();

            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0,
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
        if (isDead || isKnockback || isBubbleTrapped)
        {
            playerRb.linearVelocity = new Vector3(0, playerRb.linearVelocity.y, 0);
            return;
        }

        float moveX = Input.GetAxis(horizontalAxis);
        float currentForward = isBubbleTrapped ? forwardSpeed * 0.5f : forwardSpeed;
        float currentSide = isBubbleTrapped ? 0 : moveX * sideSpeed;

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

            if (dustVFX != null && forwardSpeed == normalSpeed)
            {
                dustVFX.Play();
            }
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
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {

            if (isShieldActive || forwardSpeed > normalSpeed)
            {
                Debug.Log("Shield Active! Destroying obstacle.");
                Destroy(other.gameObject);
                return;
            }

            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().SpeedBoost(); // หรือสร้างฟังก์ชัน Stun แยกต่างหาก
            }

            if (Time.time > lastHitTime + hitCooldown)
            {
                lastHitTime = Time.time;
                TakeDamage();
                isKnockback = true;

                playerRb.linearVelocity = new Vector3(0, playerRb.linearVelocity.y, 0);

                Vector3 knockbackForce = new Vector3(0, 2f, -4f);
                playerRb.AddForce(knockbackForce, ForceMode.Impulse);

                CancelInvoke("EndKnockback");
                Invoke("EndKnockback", 0.7f);
            }
        }

        if (other.CompareTag("Heart"))
        {
            AddHealth(1);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage()
    {
        if (isShieldActive) return;

        GameManager.instance.TakeDamage(1, playerID); 
        health = GameManager.instance.GetCurrentHealth(playerID);
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }
    public void SpeedBoost()
    {
        CancelInvoke("ResetSpeed");
        forwardSpeed = normalSpeed + 5f;

        if (lightningVFX != null) lightningVFX.Play();
        if (speedLinesVFX != null) speedLinesVFX.Play();
        if (dustVFX != null) dustVFX.Stop();

        Invoke("ResetSpeed", 5f);
    }

    void ResetSpeed()
    {
        forwardSpeed = normalSpeed;

        if (lightningVFX != null) lightningVFX.Stop();
        if (speedLinesVFX != null) speedLinesVFX.Stop();
        if (dustVFX != null && jumpCount == 0)
        {
            dustVFX.Play();
        }
    }

    public void AddHealth(int amount)
    {
        GameManager.instance.AddHealth(amount, playerID);
        health = GameManager.instance.GetCurrentHealth(playerID);
        Debug.Log("Health: " + health);
    }

    public void ActivateShield()
    {
        StopCoroutine("ShieldRoutine");
        StartCoroutine(ShieldRoutine(5f));
    }

    IEnumerator ShieldRoutine(float duration)
    {
        isShieldActive = true;
        if (shieldEffect != null) shieldEffect.SetActive(true);

        yield return new WaitForSeconds(duration);

        isShieldActive = false;
        if (shieldEffect != null) shieldEffect.SetActive(false);
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

    public void LaunchTornado()
    {
        Debug.Log("Player " + playerID + "Tornado");

        PlayerController[] allPlayers = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
        foreach (PlayerController target in allPlayers)
        {
            if (target.playerID != this.playerID)
            {
                Vector3 spawnPos = new Vector3(target.transform.position.x, 0.5f, target.transform.position.z + 2f);
                if (tornadoPrefab != null)
                {
                    Instantiate(tornadoPrefab, spawnPos, Quaternion.identity);
                    Debug.Log("Tornado Spawned at: " + spawnPos);
                }
                break;
            }
        }
    }
    public void LaunchBubble()
    {
        Debug.Log("Player " + playerID + "Bubble!");

        PlayerController[] allPlayers = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
        foreach (PlayerController target in allPlayers)
        {
            if (target.playerID != this.playerID)
            {
                Instantiate(bubblePrefab, target.transform.position, Quaternion.identity, target.transform);
                break;
            }
        }
    }

        void Die()
    {
        isDead = true;

        playerRb.linearVelocity = Vector3.zero;
        forwardSpeed = 0;

        animator.SetFloat("Speed", 0);
        animator.SetTrigger("Die");

        //Invoke("GameOver", 2f);
    }
}