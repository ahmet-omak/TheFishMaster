using UnityEngine;
using DG.Tweening;

public class FishController : MonoBehaviour,ITweenable
{
    /*TODOS:
     * Reset fish parameters each time new fish instantiated
     * Make a func. to set fish parameters
     */
    [SerializeField] FishData fishData;

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Tweener fishTweener;

    private void Awake()
    {
        //Reference Components
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();

        //Assign Sprite
        spriteRenderer.sprite = fishData.Fish.sprite;

        InitDOTween();
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

        float randomPosY = Random.Range(fishData.Fish.minPosY, fishData.Fish.maxPosY);
        float endValue = fishData.Fish.moveDirection == Fish.Direction.Right ? fishData.ScreenBoundX : -fishData.ScreenBoundX;
        float startScaleX = fishData.Fish.moveDirection == Fish.Direction.Right ? fishData.ScaleX : -fishData.ScaleX;

        transform.position = new Vector2(transform.position.x, randomPosY);
        transform.localScale = new Vector2(startScaleX, transform.localScale.y);

        fishTweener = transform.DOMoveX(endValue, fishData.Fish.movingTime).SetDelay(fishData.Fish.beginDelayTime).SetLoops(-1, LoopType.Yoyo).OnStepComplete(delegate
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        });
    }

    public void InitDOTween()
    {
        DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
        DOTween.defaultEaseType = Ease.Linear;
    }
}
