using UnityEngine;

[CreateAssetMenu(menuName = "The Fish Master/Create A Fish Data", fileName = "FishControllerData")]
public class FishData : ScriptableObject
{
    [Header("Default Values")]

    [SerializeField] float screenBoundX;
    [SerializeField] float scaleX;
    [SerializeField] Fish fish;

    public float ScreenBoundX { get => screenBoundX; }
    public float ScaleX { get => scaleX; }
    internal Fish Fish { get => fish; set => fish = value; }
}
