using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Joystick joystick;
    public float joystickHorizontalSensitivity;
    public float horizontalForce;
    public float joystickVerticalSensitivity;
    public float verticalForce;

    public bool isGrounded;

    public Transform spawnPoint;

    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (isGrounded)
        {
            if (joystick.Horizontal > joystickHorizontalSensitivity)
            {
                // move right
                rigidBody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                spriteRenderer.flipX = false;
                animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Horizontal < -joystickHorizontalSensitivity)
            {
                // move left
                rigidBody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                spriteRenderer.flipX = true;
                animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Vertical > joystickVerticalSensitivity)
            {
                // jump
                rigidBody2D.AddForce(Vector2.up * verticalForce * Time.deltaTime);
                animator.SetInteger("AnimState", 2);
            }
            else
            {
                // idle
                animator.SetInteger("AnimState", 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // respawn
        if(collision.CompareTag("DeathPlane"))
            transform.position = spawnPoint.position;
    }
}
