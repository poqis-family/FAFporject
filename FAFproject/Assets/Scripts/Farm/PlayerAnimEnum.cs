using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEnum
{
    public enum  PlayerDirection
    {
        Up,//0
        Down,//1
        Left,
        Right,
    } 

    public enum  PlayerAnimStage
    {
        Idle,//0
        Walking,//1
    } 
    public enum  Liftable
    {

        disable,//0
        enable,//1
    } 
}
