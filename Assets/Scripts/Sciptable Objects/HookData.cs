using UnityEngine;

[CreateAssetMenu(menuName = "The Fish Master/Hook Data", fileName = "HookControllerData", order = 0)]
public class HookData : ScriptableObject
{
    [Range(1f, 10f)]
    [SerializeField] float fishingDownwardsTime;

    [Range(1f, 10f)]
    [SerializeField] float fishingUpwardsTime;

    [Range(0f, 1f)]
    [SerializeField] float fishingUpwardsBurstTime;

    [SerializeField] float hookLength;

    [SerializeField] float hookStrength;

    [Tooltip("Time to wait after fishing in real seconds")]
    [SerializeField] float waitTime;

    public float FishingDownwardsTime { get => fishingDownwardsTime; }
    public float FishingUpwardsTime { get => fishingUpwardsTime; }
    public float FishingUpwardsBurstTime { get => fishingUpwardsBurstTime; }
    public float HookLength { get => hookLength; }
    public float HookStrength { get => hookStrength; }
    public float WaitTime { get => waitTime; }
}
