using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalData
{
    public int UID;
    public AnimalEnum.animalType animalType;
    public int animalAge_days;
    public bool isGrownUp;
    public int outputTimmer_days;
    public int ouputable;
    public bool isFull;
    public List<int> eatenList=new List<int>();
    public AnimalEnum.animalMoods mood;
    public int houseUID;
    public int relationPoint;
    public int isTouched;
    public int isInteracted;
}
