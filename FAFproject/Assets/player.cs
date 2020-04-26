using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Vector2 direction;
    private Rigidbody2D rb;
    public float speed;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        AnimatorMovement(direction);
    }
    public void AnimatorMovement(Vector2 derection)
    {
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }
    private void FixedUpdate()
    {
        rb.velocity = direction.normalized * speed; ;
    }
    private void GetInput() {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }
}
