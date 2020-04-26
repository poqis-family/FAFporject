using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    public GameObject mainControler;
    public Transform CellFolder;
    public GameObject CellPrefab;
    public GameObject MapDropDown;
    private GameObject mapObject;
    public float mapLengthX;
    public float mapLengthY;
    public float cellLengthX;
    public float cellLengthY;
    private float cellNumX;
    private float cellNumY;
    // Start is called before the first frame update
    void Start()
    {
        mapObject=FindChild.FindTheChild(mainControler, "Map");
        mapLengthX = mapObject.transform.localScale.x * mapObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        mapLengthY = mapObject.transform.localScale.y * mapObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        cellLengthX = 0.2f;
        cellLengthY = 0.2f;
        cellNumX = mapLengthX / cellLengthX;
        cellNumY = mapLengthY / cellLengthY;
        for (int y = 0; y< cellNumY; y++)
        {
            for (int x = 0; x < cellNumX; x++)
            {
                GameObject nowPrafab=Instantiate(CellPrefab, new Vector3(x * cellLengthX + mapLengthX / 2 * -1 + cellLengthX / 2, mapLengthY / 2 - cellLengthY / 2 - y * cellLengthY, 0), Quaternion.identity, CellFolder);
                nowPrafab.name = x.ToString() + "," + y.ToString();
            }
        }
    }

    public void ChangeMap()
    {
        switch (MapDropDown.GetComponent<Dropdown>().value)
        {
            case 0:
                MapAttribute.NowMapArr = MapAttribute.MapHomeArr;
                FindChild.FindTheChild(mainControler,"Map");
                break;

             case 1:
                MapAttribute.NowMapArr = MapAttribute.MapFarmArr;
                break;

            case 2:
                MapAttribute.NowMapArr = MapAttribute.MapSquareArr;
                break;
            case 3:
                MapAttribute.NowMapArr = MapAttribute.MapForestArr;
                break;

            default:
                break;
        }

    }
    public void MapSetter()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }
}
