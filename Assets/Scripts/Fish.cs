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
    public Sprite sprite;
    public float count;
    public int price;
    [Range(1f, 3f)] public float movingTime;
    [Range(0f, 3f)] public float beginDelayTime;
    [Range(-20f, -50f)] public float minPosY;
    [Range(-20f, -50f)] public float maxPosY;
}
