using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour { 
    public float speed = 150;
    public float jumpForce = 7;
    public bool lookAtCursor;
    public KeyCode leftButton = KeyCode.A;
    public KeyCode rightButton = KeyCode.D;
    public KeyCode addForceButton = KeyCode.Space;
    public bool isFacingRight = true;
    private Vector3 direction;
    private float horizontal;
    private Rigidbody2D rb;
    private Animator animator;
    private bool jump;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.fixedAngle = true;
    }

    void FixedUpdate()
    {
        rb.AddForce(direction * rb.mass * speed);

        if (Mathf.Abs(rb.velocity.x) > speed / 100f)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * speed / 100f, rb.velocity.y);
        }

        if (jump)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f);

        if (Input.GetKeyDown(addForceButton))
        {
            jump = true;
        }

        if (Input.GetKey(leftButton)) horizontal = -1;
        else if (Input.GetKey(rightButton)) horizontal = 1; else horizontal = 0;

        direction = new Vector2(horizontal, 0);

        if (horizontal > 0 && !isFacingRight) Flip(); else if (horizontal < 0 && isFacingRight) Flip();
    }
}