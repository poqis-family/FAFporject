using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WJExcelDataClass;
using UnityEngine.UI;
using WJExcelDataManager;

public class MainDialog : MonoBehaviour
{

    public GameObject CanvasGameObject;
    public GameObject BGGameObject;
    public GameObject FullScreeAnimationGameObject;
    public static int nowDialogID;
    //int dialogID;
    public void OperaSenceJumper(int getID)
    {
        nowDialogID = getID;
        AsyncOperation test = SceneManager.LoadSceneAsync("Opera");
        //赋值ID并且跳转至剧情场景
    }
    public void DialogReduction(int dialogID)
    {
        DataManager dataManager = new DataManager();
        dataManager.LoadAll();

        Dialog1Item nowDialogueData = dataManager.GetDialog1ItemByID(dialogID);
        CharactersItem role1ID = dataManager.GetCharactersItemByID(nowDialogueData.role1ID);
        CharactersItem role2ID = dataManager.GetCharactersItemByID(nowDialogueData.role2ID);
        //获取指定对话的数据、获取角色1、角色2数据

        GameObject objRole1 = FindChild.FindTheChild(CanvasGameObject, "Role1");
        objRole1.GetComponent<RawImage>().texture = Resources.Load("Characters/" + role1ID.image + nowDialogueData.Imageface1, typeof(Texture)) as Texture;
        GameObject objRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2");
        objRole2.GetComponent<RawImage>().texture = Resources.Load("Characters/" + role2ID.image + nowDialogueData.Imageface2, typeof(Texture)) as Texture;
        //替换了两个角色的图片
        FindChild.FindTheChild(CanvasGameObject, "SayerName").GetComponent<Text>().text = dataManager.GetCharactersItemByID(nowDialogueData.sayerID).name;
        //更换了说话者的名字
        FindChild.FindTheChild(CanvasGameObject, "DialogText").GetComponent<Text>().text = nowDialogueData.dialogText;
        FindChild.FindTheChild(CanvasGameObject, "DialogText").GetComponent<Text>().fontSize = nowDialogueData.textSize;
        //更改了文本内容与字号

        FindChild.FindTheChild(CanvasGameObject, "BGM").GetComponent<AudioSource>().clip = Resources.Load("BGM/" + nowDialogueData.Bgm, typeof(AudioClip)) as AudioClip;
        FindChild.FindTheChild(CanvasGameObject, "BGM").GetComponent<AudioSource>().Play();
        //完成了BGM的播放
        FindChild.FindTheChild(CanvasGameObject, "RoleVoice").GetComponent<AudioSource>().clip = Resources.Load("Voice/" + nowDialogueData.sound, typeof(AudioClip)) as AudioClip;
        FindChild.FindTheChild(CanvasGameObject, "RoleVoice").GetComponent<AudioSource>().Play();
        //完成了语音/音效的播放

        switch (nowDialogueData.ScreenAnimation)
        {
            case 1://背景震动
                ScreenAnimation.DoShake(BGGameObject);
                break;
            case 3://黑屏闪烁
                ScreenAnimation.DoBlackTwinkle(FullScreeAnimationGameObject);
                break;
            case 4://白屏闪烁
                ScreenAnimation.DoWhiteTwinkle(FullScreeAnimationGameObject);
                break;

        }
        //完成了全屏特效


        Quaternion rotationRole1 = FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localRotation;
        switch (nowDialogueData.ImageState1[0])
        {
            case 0:
                rotationRole1.y = 0;
                FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localRotation = rotationRole1;
                break;

            case 1:
                rotationRole1.y = 180;
                FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localRotation = rotationRole1;
                break;
        }//完成了role1的是否翻转读表

        Quaternion rotationRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localRotation;
        switch (nowDialogueData.ImageState2[0])
        {
            case 0:
                rotationRole2.y = 0;
                FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localRotation = rotationRole2;
                break;

            case 1:
                rotationRole2.y = 180;
                FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localRotation = rotationRole2;
                break;
        }//完成了role2的是否翻转读表

        Vector3 posRole1 = FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localPosition;
        switch (nowDialogueData.ImageState1[1])
        {
            case -3://左侧画面外
                posRole1.x = -750;
                FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localPosition = posRole1;
                break;
            case -2://左侧靠边
                posRole1.x = -365;
                FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localPosition = posRole1;
                break;
            case -1://左侧中间
                posRole1.x = -250;
                FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localPosition = posRole1;
                break;
            case 0://画面中间
                posRole1.x = 0;
                FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localPosition = posRole1;
                break;
            case 1://右侧中间
                posRole1.x = 250;
                FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localPosition = posRole1;
                break;
            case 2://右侧靠边
                posRole1.x = 365;
                FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localPosition = posRole1;
                break;
            case 3://右侧画面外
                posRole1.x = 750;
                FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localPosition = posRole1;
                break;
        }//完成了role1的显示位置读表

        Vector3 posRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localPosition;
        switch (nowDialogueData.ImageState2[1])
        {
            case -3://左侧画面外
                posRole2.x = -750;
                FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localPosition = posRole2;
                break;
            case -2://左侧靠边
                posRole2.x = -365;
                FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localPosition = posRole2;
                break;
            case -1://左侧中间
                posRole2.x = -250;
                FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localPosition = posRole2;
                break;
            case 0://画面中间
                posRole2.x = 0;
                FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localPosition = posRole2;
                break;
            case 1://右侧中间
                posRole2.x = 250;
                FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localPosition = posRole2;
                break;
            case 2://右侧靠边
                posRole2.x = 365;
                FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localPosition = posRole2;
                break;
            case 3://右侧画面外
                posRole2.x = 750;
                FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localPosition = posRole2;
                break;
        }//完成了role2的显示位置读表

        Vector3 scaleRole1 = FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localScale;
        scaleRole1.x = (float)nowDialogueData.ImageState1[2] / 100;
        scaleRole1.y = scaleRole1.x;
        FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localScale = scaleRole1;

        Vector3 scaleRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localScale;
        scaleRole2.x = (float)nowDialogueData.ImageState2[2] / 100;
        scaleRole2.y = scaleRole2.x;
        FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localScale = scaleRole2;


    }
    public bool IsLastDialog(int nowDialogueID)
    {
        bool isLastDialog;
        Dialog1Item nextDialogueData;
        DataManager dataManager = new DataManager();
        dataManager.LoadAll();


        nextDialogueData = dataManager.GetDialog1ItemByID(nowDialogueID + 1);
        if (nextDialogueData == null) isLastDialog = true;
        else isLastDialog = false;

        return isLastDialog;

    }
     void Start()
    {
        CanvasGameObject = GameObject.FindWithTag("Canvas");
        BGGameObject = FindChild.FindTheChild(CanvasGameObject, "BG");
        FullScreeAnimationGameObject = FindChild.FindTheChild(CanvasGameObject, "FullScreeAnimation");
        //运行时对Canvas的GameObject进行绑定

        //**** 可以在此处插入剧情开始流程****//

        DialogReduction(nowDialogID);
        //加载第一句对话
    }
    void Update()
    {

    }
    public void nextSentence()
    {
        if (IsLastDialog(nowDialogID) == false)
        {
            Debug.Log("123321");
            DialogReduction(++nowDialogID);
        }
    }
}
