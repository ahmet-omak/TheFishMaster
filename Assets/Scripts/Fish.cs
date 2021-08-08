using System;
using UnityEngine;

[Serializable]
class Fish
{
    public enum Direction
    {
        Right, Left
    }

    [Header("Normal Values")]
    public Direction moveDirection;
    public int price;

    [Header("Debug Values")]
    public float movingTime;
    public float beginDelayTime;
    public float minPosY;
    public float maxPosY;
    public float endValueX;
    public float randomPosY;
    public float startScaleX;
}
