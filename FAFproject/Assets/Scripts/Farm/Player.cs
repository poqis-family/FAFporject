using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using WJExcelDataClass;

public class Player : MonoBehaviour
{
    public Vector2 direction;
    private Rigidbody2D _rigidbody2D;
    public Animator animator;
    public static Player _Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        _Instance = this;
    }
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        GameObject playerSprite= FindChild.FindTheChild(gameObject, "PlayerSprite");
        animator = playerSprite.GetComponent<Animator>();
        animator.SetInteger("DirectionEnum", (int) PlayerAnimEnum.PlayerDirection.Down);//初始位置向下
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (FarmSceneManager._Instance.startLoaded == false)
        {
            if (other.name=="PlayersHome")
            {
                if (Input.GetMouseButton(0))
                {
                    FarmSceneManager._Instance.SceneJump(SceneEnum.Scenes.Home);
                }
            } 
            if (other.name=="ExitToFarm")
            {
                FarmSceneManager._Instance.SceneJump(SceneEnum.Scenes.Farm);
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Items"))
        {
            FarmDataManager._Instance.ItemAdd(10001, 1);
            FarmDataManager._Instance.ItemAdd(10002, 1);
            FarmDataManager._Instance.ItemAdd(10003, 1);
            FarmDataManager._Instance.ItemAdd(10004, 1);
            FarmDataManager._Instance.ItemAdd(10005, 1);
        }
    }
    
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = direction.normalized * FarmDataManager._Instance.playerData.MoveSpeed;
        
    }
    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //判断是否是点击事件
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin,ray.direction*999999);

            ReapCorps(ray);


        }
        if (Input.GetKeyDown(KeyCode.F))//后续所有道具交互类都放在这里了
        {
            if (BackpackData.nowItemID!=0)
            {
             UseItem();   
            }
        }
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            animator.SetInteger("DirectionEnum", (int)PlayerAnimEnum.PlayerDirection.Up);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            animator.SetInteger("DirectionEnum", (int) PlayerAnimEnum.PlayerDirection.Down);
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            animator.SetInteger("DirectionEnum", (int) PlayerAnimEnum.PlayerDirection.Left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            animator.SetInteger("DirectionEnum", (int) PlayerAnimEnum.PlayerDirection.Right);
        }

        //Debug.Log(direction);
        if (direction.Equals(Vector2.zero))
        {
            animator.SetInteger("AnimStageEnum", (int) PlayerAnimEnum.PlayerAnimStage.Idle);
        }   
        else if (!direction.Equals(Vector2.zero))
        {
            animator.SetInteger("AnimStageEnum", (int) PlayerAnimEnum.PlayerAnimStage.Walking);
        }
        
    }

    public void CheckItemLiftable()
    {
        SpriteRenderer itemImg = FindChild.FindTheChild(gameObject, "PropIMG").GetComponent<SpriteRenderer>();

        if (BackpackData.nowItemID==0||FarmDataManager._Instance.dataManager.GetPropsItemByID(BackpackData.nowItemID).isLiftable==0)
        {
            itemImg.sprite = null;
            animator.SetInteger("LiftableEnum", (int) PlayerAnimEnum.Liftable.disable);//不可举起
        }
        else if (FarmDataManager._Instance.dataManager.GetPropsItemByID(BackpackData.nowItemID).isLiftable==1)
        {
            Sprite temp= Resources.Load("UI/ItemIMG/" + FarmDataManager._Instance.dataManager.GetPropsItemByID(BackpackData.nowItemID).img,typeof(Sprite)) as Sprite;
            itemImg.sprite = temp;
            animator.SetInteger("LiftableEnum", (int) PlayerAnimEnum.Liftable.enable);//可举起
        }
    }

    private void UseItem()
    {
        PropsItem thisItemData = FarmDataManager._Instance.dataManager.GetPropsItemByID(BackpackData.nowItemID);
        UseItemIfTools(thisItemData);
        UseItemIfSeeds(thisItemData);
        UseItemIfFoods(thisItemData);
    }

    private void UseItemIfTools(PropsItem thisItemData)
    {
        if (thisItemData.mainType==(int)ItemTypeEnum.MainItemType.tools)
        {
            UseItemIfHoe(thisItemData);
            UseItemIfWateringCan(thisItemData);
        }
    }
    private void UseItemIfHoe(PropsItem thisItemData)
    {
        if (thisItemData.subType==(int)ItemTypeEnum.ToolsType.hoe)
        {
            if (FarmDataManager._Instance.VitalityConsume(thisItemData.para1,thisItemData.para2))
            {
                if (TileMapController._Instance.CheckArable(Vector3Int.FloorToInt(gameObject.transform.position)))
                {
                    TileMapController._Instance.PlowLand(Vector3Int.FloorToInt(gameObject.transform.position)); 
                } 
            }
        }
    }
    private void UseItemIfWateringCan(PropsItem thisItemData)
    {
        if (thisItemData.subType==(int)ItemTypeEnum.ToolsType.wateringCan)
        {
            if (FarmDataManager._Instance.VitalityConsume(thisItemData.para1,thisItemData.para2))
            {
                if (TileMapController._Instance.CheckWaterable(Vector3Int.FloorToInt(gameObject.transform.position)))
                {
                    TileMapController._Instance.WateringLand(Vector3Int.FloorToInt(gameObject.transform.position));
                }
            }
        }
    }
    private void UseItemIfSeeds(PropsItem thisItemData)
    {
        if (thisItemData.mainType == (int) ItemTypeEnum.MainItemType.seeds)
        {
            if (TileMapController._Instance.CheckSownable(Vector3Int.FloorToInt(gameObject.transform.position)))
            {
                TileMapController._Instance.SowingSeed(Vector3Int.FloorToInt(gameObject.transform.position), thisItemData.para1);
            }
        }
    }
    private void UseItemIfFoods(PropsItem thisItemData)
    {
        if (thisItemData.mainType == (int) ItemTypeEnum.MainItemType.foods)
        {
            FarmDataManager._Instance.VitalittRegain(thisItemData.para1,thisItemData.para1);//后续要做随机区间就配个para2再改一下这里
            FarmDataManager._Instance.ItemReduce(BackpackData.nowBackpackPage,BackpackData.nowBackpackIndex);
        }
    }
    
    private void ReapCorps(Ray ray)
    {
        RaycastHit2D hitInfo =new RaycastHit2D();
        hitInfo=Physics2D.GetRayIntersection(ray,999999.0f,LayerMask.GetMask("Crops"));//有个返回数组的方法GetRayIntersectionAll，以后可以考考虑使用
        
        
        if (hitInfo.transform != null && hitInfo.transform.CompareTag("Crops"))
        {
            Vector3 pos = hitInfo.transform.position;
            pos.x -= 0.5f;
            pos.y -= 0.5f;
            if (FarmDataManager._Instance.TryGetNowPlotDataDic().TryGetValue(Vector3Int.FloorToInt(pos),
                out PlotData plot))
            {
                if ((bool) plot.CheckCropMature())
                {
                    //清空sprite、播放收获动画、删除ganmeobject
                    hitInfo.transform.gameObject.GetComponent<SpriteRenderer>().sprite = null;
                    Animator animator = hitInfo.transform.gameObject.GetComponent<Animator>();
                    animator.enabled = true;
                    animator.Play("ReapCrop", 0, 0);
                    AnimationClip temp =
                        Resources.Load("Animation/Farm/ReapCrop", typeof(AnimationClip)) as AnimationClip;
                    //清除Game Object（TileMapController)
                    Destroy(hitInfo.transform.gameObject, temp.length);
                    //Item++(FarmDataManager)
                    int fruitItemID = FarmDataManager._Instance.dataManager.GetCropsItemByID(plot.cropID).fruitItemID;
                    FarmDataManager._Instance.ItemAdd(fruitItemID, 1);
                    //清除plotData中的crop数据(FarmDataManager)
                    FarmDataManager._Instance.DeleteCropData(Vector3Int.FloorToInt(pos));
                }
            }
        }
    }
}
