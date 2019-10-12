using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WJExcelDataClass;
using UnityEngine.UI;
using WJExcelDataManager;

public class MainDialog : MonoBehaviour
{

    public GameObject CanvasGameObject;

    public void DialogCreater(int dialogID)
    {
        DataManager dataManager = new DataManager();
        dataManager.LoadAll();
        
        Dialog1Item nowDialogueData = dataManager.GetDialog1ItemByID(dialogID);
        CharactersItem role1Data = dataManager.GetCharactersItemByID(nowDialogueData.role1ID);
        CharactersItem role2Data = dataManager.GetCharactersItemByID(nowDialogueData.role2ID);
        //获取指定对话的数据、获取角色1、角色2数据

        GameObject objRole1 = FindChild.FindTheChild(CanvasGameObject, "Role1");
        objRole1.GetComponent<RawImage>().texture = Resources.Load("Characters/"+ role1Data.image, typeof(Texture)) as Texture;
        GameObject objRole2 = FindChild.FindTheChild(CanvasGameObject, "Role2");
        objRole2.GetComponent<RawImage>().texture = Resources.Load("Characters/" + role2Data.image, typeof(Texture)) as Texture;
        //替换了两个角色的图片
        FindChild.FindTheChild(CanvasGameObject, "SayerName").GetComponent<Text>().text = dataManager.GetCharactersItemByID(nowDialogueData.sayerID).name;
        //更换了说话者的名字
        FindChild.FindTheChild(CanvasGameObject, "DialogText").GetComponent<Text>().text = nowDialogueData.dialogText;
        FindChild.FindTheChild(CanvasGameObject, "DialogText").GetComponent<Text>().fontSize = nowDialogueData.textSize;
        //更改了文本内容与字号

        FindChild.FindTheChild(CanvasGameObject,"BGM").GetComponent<AudioSource>().clip= Resources.Load("BGM/" + nowDialogueData.Bgm, typeof(AudioClip)) as AudioClip;
        FindChild.FindTheChild(CanvasGameObject,"BGM").GetComponent<AudioSource>().Play();
        //完成了BGM的播放
        FindChild.FindTheChild(CanvasGameObject, "RoleVoice").GetComponent<AudioSource>().clip = Resources.Load("Voice/" + nowDialogueData.sound, typeof(AudioClip)) as AudioClip;
        FindChild.FindTheChild(CanvasGameObject, "RoleVoice").GetComponent<AudioSource>().Play();
        //完成了语音/音效的播放

        switch (nowDialogueData.ScreenAnimation)
        {
            case 1://背景震动

                break;

        }
    }
    private void Start()
    {
        DialogCreater(10001001);
    }
}
