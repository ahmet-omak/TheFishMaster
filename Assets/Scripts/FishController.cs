using UnityEngine;
using DG.Tweening;

public class FishController : MonoBehaviour, ITweenable
{
    [SerializeField] FishData fishData;
    [SerializeField] Fish fish;

    private SpriteRenderer spriteRenderer;
    private Tweener fishTweener;

    private void Awake()
    {
        //Reference Components
        spriteRenderer = GetComponent<SpriteRenderer>();

        InitDOTween();

        SetParameters();
    }

    private void Start()
    {
        Move();
    }

    private void Move()
    {
        if (fishTweener != null)
        {
            fishTweener.Kill(false);
        }
        fishTweener = transform.DOMoveX(fishData.EndValueX, fish.movingTime)
            .SetDelay(fish.beginDelayTime)
            .SetLoops(-1, LoopType.Yoyo)
            .OnStepComplete(delegate
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        });
    }

    private void SetParameters()
    {
        //All Parameters For Current Fish Object
        fish.minPosY = Random.Range(-30f, -50f);
        fish.maxPosY = Random.Range(-30f, -50f);
        fishData.RandomPosY = Random.Range(fish.minPosY, fish.maxPosY);
        transform.position = new Vector2(transform.position.x, fishData.RandomPosY);

        fish.moveDirection = Random.Range(0, 2) == 0 ? Fish.Direction.Right : Fish.Direction.Left;
        fishData.EndValueX = fish.moveDirection == Fish.Direction.Right ? fishData.ScreenBoundX : -fishData.ScreenBoundX;
        fishData.StartScaleX = fish.moveDirection == Fish.Direction.Right ? fishData.ScaleX : -fishData.ScaleX;
        transform.localScale = new Vector2(fishData.StartScaleX, transform.localScale.y);

        spriteRenderer.sprite = fishData.FishSprites[Random.Range(0, fishData.FishSprites.Count)];

        fish.movingTime = Random.Range(1f, 3f);
        fish.beginDelayTime = Random.Range(0f, 3f);
        fish.count = Random.Range(1, 5);
        fish.price = Random.Range(100, 1000);
    }

    public void InitDOTween()
    {
        DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
        DOTween.defaultEaseType = Ease.Linear;
    }
}
