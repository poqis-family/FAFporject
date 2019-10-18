using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 引用DoTween
using DG.Tweening;

public static class ScreenAnimation
{
    // 震动强度
    private static Vector3 m_shakeStrength = new Vector3(22, 22, 0);

    // 缩放类型
    private static Ease m_myEase = Ease.InOutBack;
    public static Ease MyEase { get => m_myEase; set => m_myEase = value; }

    public static void DoShake(GameObject targetGameObject)
    {
        //Debug.Log("震动事件" + m_imgTransform);
        Tweener tweener = targetGameObject.transform.DOShakePosition(1, m_shakeStrength, 10, 0);
        //设置这个Tween不受Time.scale影响
        tweener.SetUpdate(true);
        //设置移动类型
        MyEase = Ease.InOutBack;
        tweener.SetEase(m_myEase);
        //tweener.onComplete = delegate ()
        //{
        //    Debug.Log("震动事件完毕" + m_imgTransform);
        //};
    }
    public static void DoWhiteTwinkle(GameObject targetGameObject)
    {
        Color color = targetGameObject.GetComponent<Image>().color;
        Sequence mySequence = DOTween.Sequence();
        //创建了sequence列表
        color.r = 255;
        color.g = 255;
        color.b = 255;
        color.a = 0;
        //设置为白色全透明
        mySequence.Append(DOTween.ToAlpha(() => color, x => color = x, 1, 0.3f));
        mySequence.Append(DOTween.ToAlpha(() => color, x => color = x, 0, 0.3f));
        //添加了由透明至不透明至透明的过程
        mySequence.onUpdate = () => { targetGameObject.GetComponent<Image>().color = color; };
        //targetGameObject.GetComponent<Image>().color.a.
        Debug.Log("白闪事件" + targetGameObject);
        //设置这个Tween不受Time.scale影响
        mySequence.SetUpdate(true);
        //设置移动类型
        MyEase = Ease.Linear;
        mySequence.SetEase(m_myEase);
        mySequence.onComplete = delegate ()
        {
         Debug.Log("白闪事件完成" + targetGameObject);
        };
    }
    public static void DoBlackTwinkle(GameObject targetGameObject)
    {
        Color color = targetGameObject.GetComponent<Image>().color;
        Sequence mySequence = DOTween.Sequence();
        //创建了sequence列表
        color.r = 0;
        color.g = 0;
        color.b = 0;
        color.a = 0;
        //设置为黑色全透明
        mySequence.Append(DOTween.ToAlpha(() => color, x => color = x, 1, 0.3f));
        mySequence.Append(DOTween.ToAlpha(() => color, x => color = x, 0, 0.3f));
        //添加了由透明至不透明至透明的过程
        mySequence.onUpdate = () => 
        {
            targetGameObject.GetComponent<Image>().color = color;
        };
        //targetGameObject.GetComponent<Image>().color.a.
        Debug.Log("黑闪事件" + targetGameObject);
        //设置这个Tween不受Time.scale影响
        mySequence.SetUpdate(true);
        //设置移动类型
        MyEase = Ease.Linear;
        mySequence.SetEase(m_myEase);
        mySequence.onComplete = delegate ()
        {
            Debug.Log("黑闪事件完成" + targetGameObject);
        };
    }
}