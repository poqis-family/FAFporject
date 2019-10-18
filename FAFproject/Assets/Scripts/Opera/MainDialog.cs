using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WJExcelDataClass;
using UnityEngine.UI;
using WJExcelDataManager;
using DG.Tweening;

public class MainDialog : MonoBehaviour
{

    public GameObject CanvasGameObject;
    public GameObject BGGameObject;
    public GameObject FullScreeAnimationGameObject;
    public static int nowDialogueID;
    public static int originalDialogueID;
    public static Dialog1Item nowDialogueData;
    public static Dialog1Item LastDialogueData;
    public static DataManager dataManager;
    public static CharactersItem role1Data;
    public static CharactersItem role2Data;

    int i;
    //int dialogID;
    public void OperaSenceJumper(int getID)
    {
        nowDialogueID = getID;
        originalDialogueID = getID;
        AsyncOperation test = SceneManager.LoadSceneAsync("Opera");
        //赋值ID并且跳转至剧情场景
    }
    public void DialogReduction(int dialogID)
    {
        dataManager = new DataManager();
        dataManager.LoadAll();

        nowDialogueData = dataManager.GetDialog1ItemByID(dialogID);
        LastDialogueData = dataManager.GetDialog1ItemByID(dialogID-1);
        role1Data = dataManager.GetCharactersItemByID(nowDialogueData.role1ID);
        role2Data = dataManager.GetCharactersItemByID(nowDialogueData.role2ID);
        //获取指定对话的数据、获取角色1、角色2数据

        ReplaceRole1();
        ReplaceRole2();
        //替换了两个角色的图片
        SayerName();
        //更换了说话者的名字
        DialogText();
        //更改了文本内容与字号

        BGMController();
        //完成了BGM的播放
        VoiceController();
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
    }

    private void VoiceController()
    {
        if (nowDialogueData.sound != null)
        {
            FindChild.FindTheChild(CanvasGameObject, "RoleVoice").GetComponent<AudioSource>().clip = Resources.Load("Voice/" + nowDialogueData.sound, typeof(AudioClip)) as AudioClip;
            FindChild.FindTheChild(CanvasGameObject, "RoleVoice").GetComponent<AudioSource>().Play();
        }
    }

    private void BGMController()
    {
        if (nowDialogueID == originalDialogueID || nowDialogueData.Bgm != LastDialogueData.Bgm)//本句是第一句话或与上一句数据的BGM 不同时
        {
            if (nowDialogueData.Bgm == null)//如果数据为空
            {
                FindChild.FindTheChild(CanvasGameObject, "BGM").GetComponent<AudioSource>().clip = null;//将播放内容换成null
            }
            else//如果数据不为空
            {
                FindChild.FindTheChild(CanvasGameObject, "BGM").GetComponent<AudioSource>().clip = Resources.Load("BGM/" + nowDialogueData.Bgm, typeof(AudioClip)) as AudioClip;
                FindChild.FindTheChild(CanvasGameObject, "BGM").GetComponent<AudioSource>().Play();
            }//播放内容
        }


    }//完成了BGM播放，如果要播放的bgm与上一句相同，则不作任何 改变

    private void SayerName()
    {
        FindChild.FindTheChild(CanvasGameObject, "SayerName").GetComponent<Text>().text = dataManager.GetCharactersItemByID(nowDialogueData.sayerID).name;
    }

    private void DialogText()
    {
        FindChild.FindTheChild(CanvasGameObject, "DialogText").GetComponent<Text>().text = nowDialogueData.dialogText;
        FindChild.FindTheChild(CanvasGameObject, "DialogText").GetComponent<Text>().fontSize = nowDialogueData.textSize;
    }

    private void ReplaceRole2()
    {
        GameObject objRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2");
        objRole2.GetComponent<RawImage>().texture = Resources.Load("Characters/" + role2Data.image + nowDialogueData.Imageface2, typeof(Texture)) as Texture;
        //完成了Role2的加载

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

        Vector3 scaleRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localScale;
        scaleRole2.x = (float)nowDialogueData.ImageState2[2] / 100;
        scaleRole2.y = scaleRole2.x;
        FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localScale = scaleRole2;
//完成了role2的缩放
    }

    private void ReplaceRole1()
    {
        if (nowDialogueID == originalDialogueID)
        {
            ChangeRole1Image();
            //替换role1的图片

            RotateRole1();//完成了role1的是否翻转读表



            PostionRole1();//完成了role1的显示位置读表


            ScaleRole1();
            //完成了role1的缩放
        }
        else if (nowDialogueData.role1ID != LastDialogueData.role1ID)
        {
            Sequence mySequence = DOTween.Sequence();
            Ease MyEase;
            MyEase = Ease.Linear;
            mySequence.SetEase(MyEase);            //设置移动类型

            Color color = FindChild.FindTheChild(CanvasGameObject, "Role1").GetComponent<RawImage>().color;
            mySequence.AppendCallback(() => { color.a = 1; });
            mySequence.Append(DOTween.ToAlpha(() => color, x => color = x, 0, 0.4f));
            mySequence.InsertCallback(0.4f, () => { ChangeRole1Image(); Debug.Log("role1替换图片"); });
            mySequence.InsertCallback(0.4f, () => { RotateRole1(); Debug.Log("role1翻转读表"); });
            mySequence.InsertCallback(0.4f, () => { PostionRole1(); Debug.Log("role1位置读表"); });
            mySequence.InsertCallback(0.4f, () => { ScaleRole1(); Debug.Log("role1的缩放"); });
            mySequence.Insert(0.4f, DOTween.ToAlpha(() => color, x => color = x, 1, 0.4f));
            mySequence.onUpdate = () =>
            {
                FindChild.FindTheChild(CanvasGameObject, "Role1").GetComponent<RawImage>().color = color;
                Debug.Log(color.a);
            };
            mySequence.SetUpdate(true);
        }
        else
        {
            ChangeRole1Image();
            if (nowDialogueData.ImageState1[0] != LastDialogueData.ImageState1[0])
            {
                Vector3 role1Rotate = new Vector3(0, 0, 0);
                switch (nowDialogueData.ImageState1[0])
                {
                    case 0:
                        role1Rotate.y = 0;
                        break;
                    case 1:
                        role1Rotate.y = 180;
                        break;
                }
                FindChild.FindTheChild(CanvasGameObject, "Role1").transform.DOLocalRotate(role1Rotate, 0.5f);
            }
            if (nowDialogueData.ImageState1[1] != LastDialogueData.ImageState1[1])
            {
                Vector3 posRole1 = FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localPosition;
                switch (nowDialogueData.ImageState1[1])
                {
                    case -3://左侧画面外
                        posRole1.x = -750;
                        break;
                    case -2://左侧靠边
                        posRole1.x = -365;
                        break;
                    case -1://左侧中间
                        posRole1.x = -250;
                        break;
                    case 0://画面中间
                        posRole1.x = 0;
                        break;
                    case 1://右侧中间
                        posRole1.x = 250;
                        break;
                    case 2://右侧靠边
                        posRole1.x = 365;
                        break;
                    case 3://右侧画面外
                        posRole1.x = 750;
                        break;
                }
                FindChild.FindTheChild(CanvasGameObject, "Role1").transform.DOLocalMoveX(posRole1.x, 0.5f);
            }
            if (nowDialogueData.ImageState1[2] != LastDialogueData.ImageState1[2])
            {
                Vector3 scaleRole1 = FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localScale;
                scaleRole1.x = (float)nowDialogueData.ImageState1[2] / 100;
                scaleRole1.y = scaleRole1.x;
                FindChild.FindTheChild(CanvasGameObject, "Role1").transform.DOScale(scaleRole1, 0.5f);
            }
        }





     










    }

    private void ScaleRole1()
    {
        Vector3 scaleRole1 = FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localScale;
        scaleRole1.x = (float)nowDialogueData.ImageState1[2] / 100;
        scaleRole1.y = scaleRole1.x;
        FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localScale = scaleRole1;
    }

    private void PostionRole1()
    {
        Vector3 posRole1 = FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localPosition;
        switch (nowDialogueData.ImageState1[1])
        {
            case -3://左侧画面外
                posRole1.x = -750;
                break;
            case -2://左侧靠边
                posRole1.x = -365;
                break;
            case -1://左侧中间
                posRole1.x = -250;
                break;
            case 0://画面中间
                posRole1.x = 0;
                break;
            case 1://右侧中间
                posRole1.x = 250;
                break;
            case 2://右侧靠边
                posRole1.x = 365;
                break;
            case 3://右侧画面外
                posRole1.x = 750;
                break;
        }
        FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localPosition = posRole1;
    }

    private void RotateRole1()
    {
        Quaternion rotationRole1 = FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localRotation;
        switch (nowDialogueData.ImageState1[0])
        {
            case 0:
                rotationRole1.y = 0;
                break;

            case 1:
                rotationRole1.y = 180;
                break;
        }
        FindChild.FindTheChild(CanvasGameObject, "Role1").transform.localRotation = rotationRole1;
    }

    private void ChangeRole1Image()
    {
        GameObject objRole1 = FindChild.FindTheChild(CanvasGameObject, "Role1");
        objRole1.GetComponent<RawImage>().texture = Resources.Load("Characters/" + role1Data.image + nowDialogueData.Imageface1, typeof(Texture)) as Texture;
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
        i = 1;
        DialogReduction(nowDialogueID);
        //加载第一句对话
    }
    void Update()
    {

    }
    public void nextSentence()
    {
        if (IsLastDialog(nowDialogueID) == false)
        {
            Debug.Log("点击读取下一条");
            i++;
            if (i == 4)
            { i += 100; }
            DialogReduction(++nowDialogueID);

        }
    }
}
