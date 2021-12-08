using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Tile = UnityEngine.WSA.Tile;

public class Player : MonoBehaviour
{
    public Vector2 direction;
    private Rigidbody2D _rigidbody2D;
    public float speed;
    private Animator _animator;
    public static Player _Player;
    // Start is called before the first frame update
    private void Awake()
    {
        _Player = this;
    }
    void Start()
    {

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.SetInteger("DirectionEnum", (int) PlayerAnimEnum.PlayerDirection.Down);//初始位置向下
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.CompareTag("Items"))
        {
            MainData.ItemAdd(10001, 1);
            
        }

    }
    
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = direction.normalized * speed; ;
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(TileMapController._Instance.CheckSownable(Vector3Int.FloorToInt(gameObject.transform.position)));
            if (TileMapController._Instance.CheckSownable(Vector3Int.FloorToInt(gameObject.transform.position)))
            {
                TileMapController._Instance.SowingSeed(Vector3Int.FloorToInt(gameObject.transform.position));
            }
        }
        

        
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            _animator.SetInteger("DirectionEnum", (int)PlayerAnimEnum.PlayerDirection.Up);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            _animator.SetInteger("DirectionEnum", (int) PlayerAnimEnum.PlayerDirection.Down);
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            _animator.SetInteger("DirectionEnum", (int) PlayerAnimEnum.PlayerDirection.Left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            _animator.SetInteger("DirectionEnum", (int) PlayerAnimEnum.PlayerDirection.Right);
        }

        //Debug.Log(direction);
        if (direction.Equals(Vector2.zero))
        {
            _animator.SetInteger("AnimStageEnum", (int) PlayerAnimEnum.PlayerAnimStage.Idle);
        }   
        else if (!direction.Equals(Vector2.zero))
        {
            _animator.SetInteger("AnimStageEnum", (int) PlayerAnimEnum.PlayerAnimStage.Walking);
        }
        
    }
}
