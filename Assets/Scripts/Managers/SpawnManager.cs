using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] FishData fishData;
    [SerializeField] GameObject fishPrefab;

    private void Start()
    {
        SpawnFishes();
    }

    private void SpawnFishes()
    {
        for (int i = 0; i < fishData.TotalFishCount; i++)
        {
            Instantiate(fishPrefab,transform);
        }
    }
}
