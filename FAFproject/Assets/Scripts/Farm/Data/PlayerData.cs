using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int MoveSpeed=10;
    public int NormalMoveSpeed=10;//后续转表格
    public int TiredMoveSpeed=3;//后续转表格
    
    private int _maxVitality=180;
    public int maxVitality
    {
        get { return _maxVitality;}
        set
        {
            _maxVitality = value;
            nowVitality = _maxVitality;
        }
    }
    private int _nowVitality=180;
    public int nowVitality
    {
        get
        {
            return _nowVitality;
        }
        set
        {
            _nowVitality = value;
            if(_nowVitality<=0)
            {
                _nowVitality = 0;
                MoveSpeed = TiredMoveSpeed;
            }
            if(_nowVitality>0)
            {
                MoveSpeed = NormalMoveSpeed;
            }
            VitalityBarController._Instance.RefreshVitalityBar();
        }
    }
    
    private int _maxHP=200;
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
    private int _nowHP=200;
    public int nowHP
    {
        get
        {
            return _nowHP;
        }
        set
        {
            _nowHP = value;
            //后续添加：刷新UI
        }
    }
}