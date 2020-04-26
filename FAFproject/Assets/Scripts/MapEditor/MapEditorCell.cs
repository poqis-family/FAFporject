using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapEditorCell : MonoBehaviour
{
   public GameObject mainControler;
    public float mapLengthX;
    public float mapLengthY;
    public float cellLengthX;
    public float cellLengthY;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        mainControler = transform.parent.parent.gameObject;
        mapLengthX = mainControler.GetComponent<MapEditor>().mapLengthX;
        mapLengthY = mainControler.GetComponent<MapEditor>().mapLengthY;
        cellLengthX = mainControler.GetComponent<MapEditor>().cellLengthX;
        cellLengthY = mainControler.GetComponent<MapEditor>().cellLengthY;
        GameObject cellXText= FindChild.FindTheChild(mainControler, "CellXInput");
        GameObject cellYText=    FindChild.FindTheChild(mainControler, "CellYInput");
        
        cellXText.GetComponent<InputField>().text = Mathf.Round((transform.position.x + mapLengthX / 2 - cellLengthX / 2) / cellLengthX).ToString();
        cellYText.GetComponent<InputField>().text = Mathf.Round((mapLengthY / 2 - cellLengthY / 2 - transform.position.y) / cellLengthY).ToString();
        //  cellYText.GetComponent<TextEditor>().text =;
    }
}
