using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{
    private GameObject arableMap;
[SerializeField]
    private TileBase ruleTile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckArable(Vector3Int pos)
    {
        arableMap= FindChild.FindTheChild(GameObject.Find("Map"), "Tilemap");
        Tilemap tm = arableMap.GetComponent<Tilemap>();
        Debug.Log(arableMap.name);
        tm.SetTile(pos,ruleTile);
        //Sprite tileSprite= arableMap.GetComponent<Tilemap>().GetSprite(pos);
        //Debug.Log("TILE"+arableMap.GetComponent<Tilemap>().GetTile(pos).name);

    }
}
