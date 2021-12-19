using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CropObjectScript : MonoBehaviour
{
    private Animator animator;
    private int i = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {

        AnimatorStateInfo temp = animator.GetCurrentAnimatorStateInfo(0);
        if (animator.isActiveAndEnabled && temp.IsName("Done"))
        {
            animator.Play("Waiting",0,0);
            GetComponent<Animator>().enabled = false;
            Vector3 pos = transform.position; 
            pos.x -= 0.5f;
            pos.y -= 0.5f;
            if (FarmDataManager._Instance.mainData.plotDataDic.TryGetValue(Vector3Int.FloorToInt(pos),
                out PlotData plot))
            {
                TileMapController._Instance.changeCropObjectSprite(gameObject, plot.CropTileName);
            }
        }
    }
}

