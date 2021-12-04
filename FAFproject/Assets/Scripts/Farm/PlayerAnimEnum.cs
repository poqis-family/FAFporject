using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEnum : MonoBehaviour
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
    
}
