using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitalityBarController : MonoBehaviour
{
    public static VitalityBarController _Instance;
    private GameObject NowVitality;
    private GameObject MaxVitality;
    private GameObject VitalityInfo;
    private GameObject VitalityValue;
    private void Awake()
    {
        _Instance = this; 
        NowVitality =FindChild.FindTheChild(gameObject, "NowVitality");
        MaxVitality =FindChild.FindTheChild(gameObject, "MaxVitality");
        VitalityInfo =FindChild.FindTheChild(gameObject, "VitalityInfo");
        VitalityValue =FindChild.FindTheChild(gameObject, "VitalityValue");
    }

    public void RefreshVitalityBar()
    {
        int nowVitality = FarmDataManager._Instance.playerData.nowVitality;
        int maxVitality = FarmDataManager._Instance.playerData.maxVitality;
        float MaxVitalityHeight = MaxVitality.GetComponent<RectTransform>().sizeDelta.y;
        Vector2 BarHeight = new Vector2(NowVitality.GetComponent<RectTransform>().sizeDelta.x, MaxVitalityHeight * ((float)nowVitality / (float)maxVitality));
        NowVitality.GetComponent<RectTransform>().sizeDelta = BarHeight;
        VitalityValue.GetComponent<Text>().text = FarmDataManager._Instance.playerData.nowVitality + "/" +
                                                  FarmDataManager._Instance.playerData.maxVitality;
    }

    public void MouseHover()
    {
        VitalityInfo.SetActive(true);
    }
    public void MouseUnhover()
    {
        VitalityInfo.SetActive(false);
    }
}
