using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        groundDetectorMask = LayerMask.GetMask("Default");

        DontDestroyOnLoad(this);
        MainManager.Instance.player = this.gameObject;
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;

        isOnGround = Physics2D.OverlapCircle(groundDetectorTransform.position, groundDetectorRadius, groundDetectorMask); 
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            isJumping = true;
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
            Debug.Log("test");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundDetectorTransform.position, groundDetectorRadius);
    }
}
