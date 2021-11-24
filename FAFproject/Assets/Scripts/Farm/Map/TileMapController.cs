using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{
    private GameObject arableMap;

    public static TileMapController _Instance;
    private TileBase basetemp;
   // public TileBase ruleTile= tmpObj as TileBase;
   //public Object Obj;

   private void Awake()
   {
       _Instance = this;
   }

   void Start()
    {
        //ruleTile=Resources.Load("Tiles/test1/outside_A2_40",typeof())
      //  basetemp = Resources.Load("Tiles/test1/outside_A2_40", typeof(Asset)) as TileBase;
        basetemp = Resources.Load<TileBase>("Tiles/test1/outside_A2_40");
        // ruleTile= Resources.Load("Tiles/test1/outside_A2_40", typeof(asset)as object);
    }
    
    void Update()
    {

    }

    public void CheckArable(Vector3Int pos)
    {
        arableMap= FindChild.FindTheChild(GameObject.Find("Map"), "Tilemap");
        Tilemap tm = arableMap.GetComponent<Tilemap>();
     //   Debug.Log(arableMap.name); 
        //Debug.Log("!!!!"+ruleTile.name);
        
        //var basetemp = Resources.Load("Tiles/test1/outside_A2_40", typeof(TileBase));
        tm.SetTile(pos,basetemp as TileBase);
        //Sprite tileSprite= arableMap.GetComponent<Tilemap>().GetSprite(pos);
        //Debug.Log("TILE"+arableMap.GetComponent<Tilemap>().GetTile(pos).name);

    }
}
