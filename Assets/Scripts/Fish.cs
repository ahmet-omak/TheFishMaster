using UnityEngine;
using System;

[Serializable]
class Fish
{
    public enum Direction
    {
        Right, Left, EndToEnd
    }
    public Direction moveDirection;
    public int count;
    public int price;
    public float movingTime;
    public float beginDelayTime;
    public float minPosY;
    public float maxPosY;
}
