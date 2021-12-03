using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{
    private GameObject arableCheckMap;
    private GameObject PlowLayer;
    private GameObject WateringLayer;
    public static TileMapController _Instance;
    public TileBase basetemp;
   // public TileBase ruleTile= tmpObj as TileBase;
   //public Object Obj;

   private void Awake()
   {
       _Instance = this;
   }
   
    public bool CheckArable(Vector3Int pos)
    {
        arableCheckMap= FindChild.FindTheChild(GameObject.Find("Map"), "ArableCheck");
        Tilemap tm = arableCheckMap.GetComponent<Tilemap>();
        TileBase tile = tm.GetTile(pos);
        if (ReferenceEquals(tile,null))
        {
            
            Debug.LogError("arableMapTile is null");
            return false;
        }
        if (tile.name=="arable")
        {
            Debug.Log("Tilemap is arable");
            return true;
        }
        if (tile.name=="Unarable")
        {
            Debug.LogError("Tilemap is Unarable");
            return false;
        }
        
        return false;
    }

    public void PlowLand(Vector3Int pos)
    {
        PlowLayer= FindChild.FindTheChild(GameObject.Find("Map"), "PlowLayer");
        Tilemap tm = PlowLayer.GetComponent<Tilemap>();
        var basetemp = Resources.Load("Tiles/Test/grounds/PlowTile", typeof(TileBase));
        tm.SetTile(pos,basetemp as TileBase);
    }

    public bool CheckWaterable(Vector3Int pos)
    {
        PlowLayer= FindChild.FindTheChild(GameObject.Find("Map"), "PlowLayer");
        Tilemap tm = PlowLayer.GetComponent<Tilemap>();
        TileBase tile = tm.GetTile(pos);
        if (ReferenceEquals(tile,null))
        {
            Debug.LogError("Tile didnt plowed");
            return false;
        }
        if (!ReferenceEquals(tile,null))
        {
            return true;
        }
        return false;
    }

    public void WateringLand(Vector3Int pos)
    {
        WateringLayer= FindChild.FindTheChild(GameObject.Find("Map"), "WateringLayer");
        Tilemap tm = WateringLayer.GetComponent<Tilemap>();
        var basetemp = Resources.Load("Tiles/Test/grounds/WateringTile", typeof(TileBase));
        tm.SetTile(pos,basetemp as TileBase);
    }

}
