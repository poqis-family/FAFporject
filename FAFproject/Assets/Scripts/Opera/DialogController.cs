using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WJExcelDataClass;
using UnityEngine.UI;
using WJExcelDataManager;

public class DialogController:MonoBehaviour
{
    //public int DialogTrigger()
    //{
    //    int dialogID;
    //    dialogID = 10001001;
    //    int TriggerSource;
    //    TriggerSource = 0;
    //    MainDialog mainDialog = new MainDialog();
    //    mainDialog.OperaSenceJumper(dialogID);

    //    return 0;
    //}
    public void Click()
    {
        int dialogID;
        dialogID = 10001001;
        //int TriggerSource;
        //TriggerSource = 0;
        MainDialog mainDialog = new MainDialog();
        mainDialog.OperaSenceJumper(dialogID);
    }
}
