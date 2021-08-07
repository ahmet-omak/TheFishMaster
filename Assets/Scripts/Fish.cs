using System;

[Serializable]
class Fish
{
    public enum Direction
    {
        Right, Left
    }
    public Direction moveDirection;
    public int price;
    public float movingTime;
    public float beginDelayTime;
    public float minPosY;
    public float maxPosY;
    public float endValueX;
    public float randomPosY;
    public float startScaleX;
}
