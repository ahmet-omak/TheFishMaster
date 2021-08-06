using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "The Fish Master/Fish Data", fileName = "FishControllerData", order = 1)]
public class FishData : ScriptableObject
{
    [Header("Default Values")]
    [SerializeField] float screenBoundX;
    [SerializeField] float scaleX;

    [Header("Sprites For Fishes")]
    [SerializeField] List<Sprite> fishSprites;

    [Header("Do Not Edit These Values in Vains")]
    [SerializeField] float endValueX;
    [SerializeField] float randomPosY;
    [SerializeField] float startScaleX;

    public List<Sprite> FishSprites { get => fishSprites; }
    public float ScreenBoundX { get => screenBoundX; }
    public float ScaleX { get => scaleX; }

    public float EndValueX { get => endValueX; set => endValueX = value; }
    public float RandomPosY { get => randomPosY; set => randomPosY = value; }
    public float StartScaleX { get => startScaleX; set => startScaleX = value; }
}
