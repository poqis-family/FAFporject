using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class FindChild

{
    private static List<GameObject> childrenList;
    private static int foundChildCount;

    /// <summary>
    /// 从开始物体向子物体开始寻找符合名称的gameobject,未找到返回null
    /// </summary>
    /// <param name="parentObject">开始物体</param>
    /// <param name="childName">目标物体名称</param>
    /// <returns></returns>
    public static GameObject FindTheChild(GameObject parentObject, string childName)
    {
        Transform result = FindTheChild(parentObject.transform, childName);
        return result == null ? null : result.gameObject;
        //Transform temp = null;//初始化目标变量
        //if (temp == null) temp = parentObject.transform.Find(childName);//当没找到目标时，在当前物体一级子物件中寻找
        //if (temp != null)
        //{
        //    Debug.Log("Get" + temp.name);
        //    return temp.gameObject;
        //}//如果找到了就返回目标
        //else
        //{
        //    int childCount = parentObject.transform.childCount;
        //    if (childCount > 0)
        //    {
        //        for (int i = 0; i < childCount; i++)
        //        {
        //            Transform tf = parentObject.transform.GetChild(i);//未找到的话去当前物体的子物体里依次寻找
        //                                                              // string xx=tf.name;
        //            if (temp == null) temp = FindTheChild(tf, childName);//把子物体传入递归函数

        //        }
        //    }
        //}


        //if (temp == null)
        //{
        //    Debug.Log("null");
        //    return null;
        //}
        //else
        //{
        //    Debug.Log("Get" + temp.name);
        //    return temp.gameObject;
        //}

    }
    /// <summary>
    /// 用于递归的方法
    /// </summary>
    /// <param name="tf">递归父级传的transform</param>
    /// <param name="childName">寻找的目标</param>
    /// <returns></returns>
    private static Transform FindTheChild(Transform tf, string childName)
    {
        Transform temp = null;//初始化目标变量
        if (temp == null) temp = tf.transform.Find(childName);//当没找到目标时，在当前物体一级子物件中寻找
        if (temp != null)
        {
            return temp;
        }//如果找到了就返回目标
        else
        {
            int childCount = tf.transform.childCount;
            if (childCount > 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    tf = tf.transform.GetChild(i);//未找到的话去当前物体的子物体里依次寻找
                    //string xx = tf.name;
                    if (temp == null) temp = FindTheChild(tf, childName);//把子物体传入递归函数
                    tf = tf.transform.parent;//当子递归返回时需要把检查中目标从子物体返回至父物体
                }
            }
        }
        return temp;
    }


    public static GameObject[] FindTheChildren(GameObject parentObject, string targetName)
    {
        
        childrenList = new List<GameObject>();
        if (parentObject.name == targetName)
        {
            foundChildCount++;
            childrenList.Add(parentObject);
        }

        if (parentObject.transform.childCount > 0)
        {
            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                FindTheChildren(parentObject.transform.GetChild(i), targetName);
            }
        }

        if (childrenList.Count == 0)
        {
            Debug.Log("未找到该名称子物体");
            return null;
        }
        else
        {
            Debug.Log("Get" + childrenList.Count);
            return childrenList.ToArray();
        }

    }
    private static void FindTheChildren(Transform nowObjectTf, string targetName)
    {
        if (nowObjectTf.name == targetName)
        {
            foundChildCount++;
            childrenList.Add(nowObjectTf.gameObject);
        }
        if (nowObjectTf.childCount > 0)
        {
            for (int i = 0; i < nowObjectTf.transform.childCount; i++)
            {
                FindTheChildren(nowObjectTf.transform.GetChild(i), targetName);
            }
        }
       //return childrenList.ToArray();
    }
}