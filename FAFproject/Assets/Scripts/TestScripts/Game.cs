using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game
{

    public static Game current;
    public Character knight;
    public Character rogue;
    public Character wizard;

    public Game()
    {
        knight = new Character();
        rogue = new Character();
        wizard = new Character();
    }

}
