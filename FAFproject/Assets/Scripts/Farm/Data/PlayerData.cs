using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private int _maxVitality;

    public int maxVitality
    {
        get { return _maxVitality;}
        set
        {
            _maxVitality = value;
            nowVitality = _maxVitality;
        }
    }
    public int nowVitality;
    private int _maxHP;

    public int maxHP
    {
        get
        {
            return _maxHP;
        }
        set
        {
            maxHP = value;
            nowHP = maxHP;
        }

    }
    public int nowHP;
}