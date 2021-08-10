using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "The Fish Master/Fish Data", fileName = "FishControllerData", order = 1)]
public class FishData : ScriptableObject
{
    [Header("Default Values")]
    [SerializeField] float screenBoundX;
    [SerializeField] float scaleX;
    [SerializeField] int totalFishCount;

    [Header("Sprites For Fishes")]
    [SerializeField] List<Sprite> fishSprites;

    public List<Sprite> FishSprites { get => fishSprites; }
    public int TotalFishCount { get => totalFishCount; }
    public float ScreenBoundX { get => screenBoundX; }
    public float ScaleX { get => scaleX; }
}
