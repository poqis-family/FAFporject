using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListUI : MonoBehaviour
{

    public void onclick()
    {
        BackpackData.nowBackpackIndex =int.Parse(gameObject.name);
        BackpackController._Instance.RefreshItemUI();
        Player._Instance.CheckItemLiftable();
    }
}
