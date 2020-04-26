using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrasslandTest : MonoBehaviour
{
    public static float wolfNum;
    public static float rabbitNum;
    public static float grassNum;
    public static float wolfInput;
    public static float rabbitInput;
    public static float grassInput;
    public static float newWolfNum;
    public static float newRabbitNum;
    public static float newGrassNum;
    public static float rabbitReproduction;
    public static float rabbitRepRate;
    public static float grassRepRate;
    public static float wolfRepRate;
    public static float rabbitPreyRate;
  //  public static float grassPreyRate;
    public static float wolfPreyRate;
    public GameObject Canvas;
        void Start()
    {
        wolfNum = 10000f;
        rabbitNum = 10f;
        grassNum = 200f;
        rabbitRepRate = 10;
        grassRepRate = 100;
        wolfRepRate = 2;
        rabbitPreyRate = 10;
       // grassPreyRate = 5;
        wolfPreyRate = 10;
}
    public void NextYearButton()
    {
        wolfInput = float.Parse(FindChild.FindTheChild(Canvas, "WolfInput").GetComponent<Text>().text);
        rabbitInput = float.Parse(FindChild.FindTheChild(Canvas, "RabbitInput").GetComponent<Text>().text);
        grassInput = float.Parse(FindChild.FindTheChild(Canvas, "GrassInput").GetComponent<Text>().text);

        if ((wolfInput + wolfNum) * wolfRepRate <= (rabbitNum + rabbitInput) / wolfPreyRate)
            newWolfNum = (wolfInput + wolfNum) * wolfRepRate;
        else newWolfNum = (rabbitNum + rabbitInput) / wolfPreyRate;
        //比较狼*wolfRepRate和兔子除wolfPreyRate哪个小 用哪个
        if (newWolfNum < 0) newWolfNum = 0;

        if ((rabbitNum + rabbitInput) * rabbitRepRate <= (grassNum + grassInput) / rabbitPreyRate)
        {
            newRabbitNum = (rabbitNum + rabbitInput) * rabbitRepRate;
        }
        else
        {
            newRabbitNum = (grassNum + grassInput) / rabbitPreyRate;
        }

        if ((wolfInput + wolfNum) * wolfPreyRate >= newRabbitNum)
        {
            newRabbitNum = newRabbitNum * 0.1f;
        }  
        else {
            newRabbitNum = newRabbitNum - (wolfInput + wolfNum) * wolfPreyRate;
        }
        if (newRabbitNum < 0) newRabbitNum = 0;
        //比较兔子*rabbitRepRate和草除rabbitPreyRate哪个小 那个小用哪个  再减去原狼*wolfPreyRate如果数值大于现存兔子数换做兔子-90%

        if ((grassNum + grassInput) * grassRepRate + 2000 >= 30000) {
            newGrassNum = 30000;
        }
        else
        {
            newGrassNum = (grassNum + grassInput) * grassRepRate + 2000;
        }
        newGrassNum -= newRabbitNum * rabbitPreyRate;
        if (newGrassNum < 0) newGrassNum = 0;
        //草每年*grassRepRate+2000 不大于30000 再减去兔*rabbitPreyRate

        rabbitNum = newRabbitNum;
          wolfNum = newWolfNum;
        grassNum = newGrassNum;
        //newWolfNum = (wolfInput + wolfNum) * (rabbitNum + rabbitInput) / 1000f;
        //newRabbitNum = (rabbitNum + rabbitInput) / ((wolfInput + wolfNum) / 100f) * ((grassNum + grassInput) / 10000f)
        //  newGrassNum = (grassNum + grassInput) / ((rabbitNum + rabbitInput) / 1000f); 
        //  if (newWolfNum > (wolfInput + wolfNum) * 2) wolfNum = (wolfInput + wolfNum) * 2;
        //  else wolfNum = newWolfNum;
        //
        //  if (newRabbitNum > (rabbitNum + rabbitInput) * 5) rabbitNum = (rabbitNum + rabbitInput) * 5;
        //  else rabbitNum = newRabbitNum;

        //    if (newGrassNum > 30000f) grassNum = 30000f;
        //    else if (newGrassNum > (grassNum + grassInput) * 10) newGrassNum = (grassNum + grassInput) * 10;
        //     else grassNum = newGrassNum;




        FindChild.FindTheChild(Canvas, "WolfNum").GetComponent<Text>().text = wolfNum.ToString();
        FindChild.FindTheChild(Canvas, "RabbitNum").GetComponent<Text>().text = rabbitNum.ToString();
        FindChild.FindTheChild(Canvas, "GrassNum").GetComponent<Text>().text = grassNum.ToString();
    }
}
