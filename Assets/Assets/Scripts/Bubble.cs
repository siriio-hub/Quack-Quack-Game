using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float floatHeight = 1f; 
    public float duration = 3f; 

    private PlayerController targetPC;
    private Rigidbody targetRb;
    private Animator targetAnim;
    private Collider targetCollider;

    void Start()
    {
        targetPC = GetComponentInParent<PlayerController>();

        if (targetPC != null)
        {
            targetRb = targetPC.GetComponent<Rigidbody>();
            targetAnim = targetPC.GetComponent<Animator>();

            targetPC.isBubbleTrapped = true;

            if (targetCollider != null) targetCollider.isTrigger = true;
            
            if (targetRb != null)
            {
                targetRb.useGravity = false;
                targetRb.linearVelocity = new Vector3(0, 1.5f, 0);
            }

            if (targetAnim != null)
            {
                targetAnim.SetBool("isBubbled", true);
            }
        }

        Invoke("BreakBubble", duration);
    }
    void Update()
    {
        if (targetPC != null && targetPC.isBubbleTrapped && targetRb != null)
        {
            targetRb.linearVelocity = new Vector3(0, targetRb.linearVelocity.y, 0);
        }
    }

    void BreakBubble()
    {
        if (targetPC != null)
        {
            targetPC.isBubbleTrapped = false;

            if (targetCollider != null) targetCollider.isTrigger = false;

            if (targetRb != null)
            {
                targetRb.useGravity = true;
                targetRb.linearVelocity = new Vector3(0, -2f, 0); ;
            }

            if (targetAnim != null)
            {
                targetAnim.SetBool("isBubbled", false);
            }
        }

        Destroy(gameObject);
    }
}
