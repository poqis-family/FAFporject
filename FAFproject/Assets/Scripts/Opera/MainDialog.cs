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
    Sequence textAnimation;

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
        LastDialogueData = dataManager.GetDialog1ItemByID(dialogID - 1);
        role1Data = dataManager.GetCharactersItemByID(nowDialogueData.role1ID);
        role2Data = dataManager.GetCharactersItemByID(nowDialogueData.role2ID);
        //获取指定对话的数据、获取角色1、角色2数据
        LoadRole1();
        LoadRole2();
        //替换了两个角色的图片
        LoadSayerName();
        //更换了说话者的名字
        LoadDialogText();
        //更改了文本内容与字号

        LoadCG();

        LoadBG();

        LoadBGM();
        //完成了BGM的播放
        LoadVoice();
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

    private void LoadBG()
    {
        if (nowDialogueID == originalDialogueID)
        {
            FindChild.FindTheChild(CanvasGameObject, "BG").GetComponent<RawImage>().texture = Resources.Load("BG/" + nowDialogueData.backGround, typeof(Texture)) as Texture;
        }
        else if (nowDialogueData.backGround != LastDialogueData.backGround)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.AppendCallback(() =>
            { FindChild.FindTheChild(CanvasGameObject, "BGAnimate").GetComponent<RawImage>().texture = Resources.Load("BG/" + nowDialogueData.backGround, typeof(Texture)) as Texture; });
            mySequence.Append(FindChild.FindTheChild(CanvasGameObject, "BGAnimate").GetComponent<RawImage>().DOColor(new Color(255, 255, 255, 1), 0.5f));
            mySequence.AppendCallback(() =>
            { FindChild.FindTheChild(CanvasGameObject, "BG").GetComponent<RawImage>().texture = Resources.Load("BG/" + nowDialogueData.backGround, typeof(Texture)) as Texture; });
            mySequence.Append(FindChild.FindTheChild(CanvasGameObject, "BGAnimate").GetComponent<RawImage>().DOColor(new Color(255, 255, 255, 0), 0.5f));
            //bgAnimate变成新图、渐变显示，显示完成后bg换成新图，bganime变成透明
        }
    }

    private void LoadCG()
    {
        if (nowDialogueData.CGImage != null)
        {
            FindChild.FindTheChild(CanvasGameObject, "CG").GetComponent<RawImage>().texture = Resources.Load("CG/" + nowDialogueData.CGImage, typeof(Texture)) as Texture;
            FindChild.FindTheChild(CanvasGameObject, "CG").GetComponent<RawImage>().DOColor(new Color(225, 225, 225, 1), 0.5f);
        }
        else
        {
            FindChild.FindTheChild(CanvasGameObject, "CG").GetComponent<RawImage>().DOColor(new Color(225, 225, 225, 0), 0.5f);
        }
    }

    private void LoadVoice()
    {
        if (nowDialogueData.sound != null)
        {
            FindChild.FindTheChild(CanvasGameObject, "RoleVoice").GetComponent<AudioSource>().clip = Resources.Load("Voice/" + nowDialogueData.sound, typeof(AudioClip)) as AudioClip;
            FindChild.FindTheChild(CanvasGameObject, "RoleVoice").GetComponent<AudioSource>().Play();
        }
    }

    private void LoadBGM()
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

    private void LoadSayerName()
    {
        if (nowDialogueData.sayerID == 0)
            FindChild.FindTheChild(CanvasGameObject, "SayerName").GetComponent<Text>().text = null;
        else 
            FindChild.FindTheChild(CanvasGameObject, "SayerName").GetComponent<Text>().text = dataManager.GetCharactersItemByID(nowDialogueData.sayerID).name;
    }

    private void LoadDialogText()
    {
        if (nowDialogueData.textSize == 0 && nowDialogueData.dialogText == null)
        {
            FindChild.FindTheChild(CanvasGameObject, "Boxbg").SetActive(false);
        }
        else
        {
            FindChild.FindTheChild(CanvasGameObject, "Boxbg").SetActive(true);
            Text text = FindChild.FindTheChild(CanvasGameObject, "DialogText").GetComponent<Text>();
            textAnimation = DOTween.Sequence();
            textAnimation.SetUpdate(true);
            //设置移动类型
            textAnimation.SetEase(Ease.Linear);
            textAnimation.AppendCallback(() => { text.text = null; });
            textAnimation.Append(text.DOText(nowDialogueData.dialogText, nowDialogueData.dialogText.Length * 0.2f));

            if (nowDialogueData.textPositon=="居中")
            { 
            FindChild.FindTheChild(CanvasGameObject, "DialogText").GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            }
            else if(nowDialogueData.textPositon==null)
            {
                FindChild.FindTheChild(CanvasGameObject, "DialogText").GetComponent<Text>().alignment = TextAnchor.UpperLeft;
            }

            FindChild.FindTheChild(CanvasGameObject, "DialogText").GetComponent<Text>().fontSize = nowDialogueData.textSize;


        }
    }

    private void LoadRole1()
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

    private void LoadRole2()
    {
        if (nowDialogueID == originalDialogueID)
        {
            ChangeRole2Image();
            //替换role1的图片

            RotateRole2();//完成了role1的是否翻转读表



            PostionRole2();//完成了role1的显示位置读表


            ScaleRole2();
            //完成了role1的缩放
        }
        else if (nowDialogueData.role2ID != LastDialogueData.role2ID)
        {
            Sequence mySequence = DOTween.Sequence();
            Ease MyEase;
            MyEase = Ease.Linear;
            mySequence.SetEase(MyEase);            //设置移动类型

            Color color = FindChild.FindTheChild(CanvasGameObject, "Role2").GetComponent<RawImage>().color;
            mySequence.AppendCallback(() => { color.a = 1; });
            mySequence.Append(DOTween.ToAlpha(() => color, x => color = x, 0, 0.4f));
            mySequence.InsertCallback(0.4f, () => { ChangeRole2Image(); Debug.Log("role2替换图片"); });
            mySequence.InsertCallback(0.4f, () => { RotateRole2(); Debug.Log("role2翻转读表"); });
            mySequence.InsertCallback(0.4f, () => { PostionRole2(); Debug.Log("role2位置读表"); });
            mySequence.InsertCallback(0.4f, () => { ScaleRole2(); Debug.Log("role2的缩放"); });
            mySequence.Insert(0.4f, DOTween.ToAlpha(() => color, x => color = x, 1, 0.4f));
            mySequence.onUpdate = () =>
            {
                FindChild.FindTheChild(CanvasGameObject, "Role2").GetComponent<RawImage>().color = color;
                Debug.Log(color.a);
            };
            mySequence.SetUpdate(true);
        }
        else
        {
            ChangeRole2Image();
            if (nowDialogueData.ImageState2[0] != LastDialogueData.ImageState2[0])
            {
                Vector3 role2Rotate = new Vector3(0, 0, 0);
                switch (nowDialogueData.ImageState2[0])
                {
                    case 0:
                        role2Rotate.y = 0;
                        break;
                    case 1:
                        role2Rotate.y = 180;
                        break;
                }
                FindChild.FindTheChild(CanvasGameObject, "Role2").transform.DOLocalRotate(role2Rotate, 0.5f);
            }
            if (nowDialogueData.ImageState2[1] != LastDialogueData.ImageState2[1])
            {
                Vector3 posRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localPosition;
                switch (nowDialogueData.ImageState2[1])
                {
                    case -3://左侧画面外
                        posRole2.x = -750;
                        break;
                    case -2://左侧靠边
                        posRole2.x = -365;
                        break;
                    case -1://左侧中间
                        posRole2.x = -250;
                        break;
                    case 0://画面中间
                        posRole2.x = 0;
                        break;
                    case 1://右侧中间
                        posRole2.x = 250;
                        break;
                    case 2://右侧靠边
                        posRole2.x = 365;
                        break;
                    case 3://右侧画面外
                        posRole2.x = 750;
                        break;
                }
                FindChild.FindTheChild(CanvasGameObject, "Role2").transform.DOLocalMoveX(posRole2.x, 0.5f);
            }
            if (nowDialogueData.ImageState2[2] != LastDialogueData.ImageState2[2])
            {
                Vector3 scaleRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localScale;
                scaleRole2.x = (float)nowDialogueData.ImageState2[2] / 100;
                scaleRole2.y = scaleRole2.x;
                FindChild.FindTheChild(CanvasGameObject, "Role2").transform.DOScale(scaleRole2, 0.5f);
            }
        }
    }

    private void ScaleRole2()
    {
        Vector3 scaleRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localScale;
        scaleRole2.x = (float)nowDialogueData.ImageState2[2] / 100;
        scaleRole2.y = scaleRole2.x;
        FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localScale = scaleRole2;
    }

    private void PostionRole2()
    {
        Vector3 posRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localPosition;
        switch (nowDialogueData.ImageState2[1])
        {
            case -3://左侧画面外
                posRole2.x = -750;
                break;
            case -2://左侧靠边
                posRole2.x = -365;
                break;
            case -1://左侧中间
                posRole2.x = -250;
                break;
            case 0://画面中间
                posRole2.x = 0;
                break;
            case 1://右侧中间
                posRole2.x = 250;
                break;
            case 2://右侧靠边
                posRole2.x = 365;
                break;
            case 3://右侧画面外
                posRole2.x = 750;
                break;
        }
        FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localPosition = posRole2;
    }

    private void RotateRole2()
    {
        Quaternion rotationRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localRotation;
        switch (nowDialogueData.ImageState2[0])
        {
            case 0:
                rotationRole2.y = 0;
                break;

            case 1:
                rotationRole2.y = 180;
                break;
        }
        FindChild.FindTheChild(CanvasGameObject, "Role2").transform.localRotation = rotationRole2;
    }

    private void ChangeRole2Image()
    {
        GameObject objRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2");
        objRole2.GetComponent<RawImage>().texture = Resources.Load("Characters/" + role2Data.image + nowDialogueData.Imageface2, typeof(Texture)) as Texture;
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
        DialogReduction(nowDialogueID);
        //加载第一句对话
    }
    void Update()
    {

    }
    public void nextSentence()
    {
        if (textAnimation.IsPlaying())
        {
            textAnimation.Complete();
        }
        else if (IsLastDialog(nowDialogueID) == false)
        {
            Debug.Log("点击读取下一条");
            DialogReduction(++nowDialogueID);

        }
    }

    public void Skip()
    {
        Dialog1Item nextDialogueData;
        int CheckDialogueID=nowDialogueID;
        DataManager dataManager = new DataManager();
        dataManager.LoadAll();
        for (bool isLastID=false; isLastID== false;)
        {
            CheckDialogueID++;
            nextDialogueData = dataManager.GetDialog1ItemByID(CheckDialogueID);
            if (nextDialogueData == null)
            {
                isLastID = true;
                nowDialogueID = CheckDialogueID - 1;
                DOTween.CompleteAll();
                DialogReduction(nowDialogueID);
            }
        }
    }
}
