using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour
{

    public enum ProjectAxis { onlyX = 0, xAndY = 1 };
    public ProjectAxis projectAxis = ProjectAxis.onlyX;
    public float speed = 150;
    public float addForce = 7;
    public bool lookAtCursor;
    public KeyCode leftButton = KeyCode.A;
    public KeyCode rightButton = KeyCode.D;
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    public KeyCode addForceButton = KeyCode.Space;
    public bool isFacingRight = true;
    private Vector3 direction;
    private float vertical;
    private float horizontal;
    private Rigidbody2D body;
    private float rotationY;
    private bool jump;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D coll)
    {
            jump = true;
    }

    void OnCollisionExit2D(Collision2D coll)
    { 
            jump = false;
    }

    void FixedUpdate()
    {
        body.AddForce(direction * body.mass * speed);

        if (Input.GetKey(addForceButton) && jump)
        {
            body.velocity = new Vector2(0, addForce);
        }
        
    }

    void Flip()
    {
        if (projectAxis == ProjectAxis.onlyX)
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void Update()
    {

        if (Input.GetKey(upButton)) vertical = 1;
        else if (Input.GetKey(downButton)) vertical = -1; else vertical = 0;

        if (Input.GetKey(leftButton)) horizontal = -1;
        else if (Input.GetKey(rightButton)) horizontal = 1; else horizontal = 0;

        direction = new Vector2(horizontal, 0);

        if (horizontal > 0 && !isFacingRight) Flip(); else if (horizontal < 0 && isFacingRight) Flip();
    }
}