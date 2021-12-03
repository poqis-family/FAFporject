using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Tile = UnityEngine.WSA.Tile;

public class Player : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.CompareTag("Items"))
        {
            MainData.ItemAdd(10001, 1);
            
        }

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
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (TileMapController._Instance.CheckArable(Vector3Int.FloorToInt(gameObject.transform.position)))
            {
                TileMapController._Instance.PlowLand(Vector3Int.FloorToInt(gameObject.transform.position));
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (TileMapController._Instance.CheckWaterable(Vector3Int.FloorToInt(gameObject.transform.position)))
            {
                TileMapController._Instance.WateringLand(Vector3Int.FloorToInt(gameObject.transform.position));
            }
        }
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
