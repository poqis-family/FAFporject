using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListUI : MonoBehaviour
{

    public void onclick()
    {
        BackpackData.nowChooseItem =int.Parse(gameObject.name);
        BackpackController._Instance.RefreshItemUI();
    }
}
