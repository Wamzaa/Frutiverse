using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    public float moveSpeed;
    public float jumpForce;

    public Transform groundDetectorTransform;
    public float groundDetectorRadius;

    private Rigidbody2D rb;

    private float horizontalMovement;
    private bool isJumping;
    private bool isOnGround;

    private Vector3 velocity = Vector3.zero;
    private LayerMask groundDetectorMask;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Already instanciated");
        }
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        groundDetectorMask = LayerMask.GetMask("Default");
    }

    public void SetHorizontalMovement(float hMove)
    {
        horizontalMovement = hMove * moveSpeed;
    }

    public void JumpFromInput()
    {
        isOnGround = Physics2D.OverlapCircle(groundDetectorTransform.position, groundDetectorRadius, groundDetectorMask);
        if (isOnGround)
        {
            isJumping = true;
        }
    }


    private void Update()
    {
        isOnGround = Physics2D.OverlapCircle(groundDetectorTransform.position, groundDetectorRadius, groundDetectorMask);
        horizontalMovement = MainManager.Instance.GetMove() * moveSpeed;
        if (!isOnGround)
        {
            float jumpDirCoeff = 0.3f;
            horizontalMovement = jumpDirCoeff * horizontalMovement + (1.0f-jumpDirCoeff) * rb.velocity.x;
        }
    }

    private void FixedUpdate()
    {
        // --- Trouver une autre méthode plus personnalisée que SmoothDamp ---
        Vector3 targetVelocity = new Vector2(horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0.0f, jumpForce));
            isJumping = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundDetectorTransform.position, groundDetectorRadius);
    }
}
