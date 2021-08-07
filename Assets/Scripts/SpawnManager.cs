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
            fishPrefab.GetComponent<FishController>().Count = Random.Range(1, 3);
            for (int j = 0; j < fishPrefab.GetComponent<FishController>().Count; j++)
            {
                Instantiate(fishPrefab);
            }
        }
    }
}
