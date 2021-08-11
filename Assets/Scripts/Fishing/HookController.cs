using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

[SelectionBase]
public class HookController : MonoBehaviour, ITweenable
{
    [Header("Debug Values")]
    [SerializeField] bool isHookMoving;

    [Header("Normal Values")]
    [SerializeField] GameObject hookedObject;
    [SerializeField] HookData hookData;
    [SerializeField] Button hookButton;

    private new CircleCollider2D collider;
    private Tweener camTweener;
    private Camera cam;
    private Vector3 startPos;
    private List<FishController> hookedFishes;
    private int fishCount;

    private void Awake()
    {
        cam = Camera.main;
        collider = GetComponent<CircleCollider2D>();
        hookedFishes = new List<FishController>();
    }

    private void Start()
    {
        isHookMoving = false;
        collider.enabled = false;
        startPos = transform.position;
    }

    private void Update()
    {
        if (isHookMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        //Move Hook Along X-Axis
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
    }

    public void StartFishing()
    {
        UIManager.Instance.OnFishingStarted();
        hookButton.interactable = false;
        fishCount = 0;
        camTweener = cam.transform.DOMoveY(-GameManager.Instance.length, hookData.FishingDownwardsTime).OnUpdate(delegate
        {
            if (cam.transform.position.y <= -11f)
            {
                isHookMoving = true;
                transform.SetParent(cam.transform);
            }
        }).OnComplete(delegate
        {
            collider.enabled = true;
            camTweener = cam.transform.DOMoveY(0, hookData.FishingUpwardsTime).OnUpdate(delegate
             {
                 if (cam.transform.position.y >= -10f)
                 {
                     StopFishing();
                 }
             });
        });
        collider.enabled = false;
        hookedFishes.Clear();
    }

    private void StopFishing()
    {
        camTweener.Kill(false);
        camTweener = cam.transform.DOMoveY(0, hookData.FishingUpwardsBurstTime).OnUpdate(delegate
             {
                 if (cam.transform.position.y >= -11)
                 {
                     transform.SetParent(null);
                     transform.position = startPos;
                 }
             }).OnComplete(delegate
             {
                 UIManager.Instance.OnFishingStopped(hookedFishes);
                 StartCoroutine(WaitFishingRoutine(hookData.WaitTime));
             });
    }

    private IEnumerator WaitFishingRoutine(float time)
    {
        isHookMoving = false;
        yield return new WaitForSecondsRealtime(time);
        int money = 0;
        for (int i = 0; i < hookedFishes.Count; i++)
        {
            var currentHookedFish = hookedFishes[i].GetComponent<FishController>();
            currentHookedFish.transform.SetParent(null);
            currentHookedFish.SetParameters();
            currentHookedFish.Move();
            currentHookedFish.IsHooked = false;
            money += currentHookedFish.fish.price;
        }
        hookButton.interactable = true;
        collider.enabled = true;
        fishCount = 0;
    }

    public void InitDOTween()
    {
        DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
        DOTween.defaultEaseType = Ease.OutQuart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish") && fishCount != GameManager.Instance.strength)
        {
            Catch(collision);
        }
    }

    private void Catch(Collider2D collision)
    {
        //Try to catch a fish
        fishCount++;
        var fish = collision.GetComponent<FishController>();
        hookedFishes.Add(fish);
        fish.IsHooked = true;
        fish.transform.SetParent(transform);
        fish.transform.position = hookedObject.transform.position;
        fish.transform.rotation = hookedObject.transform.rotation;
        collision.transform.DOShakeRotation(hookData.WaitTime, Vector3.forward * 45, 10, 90).SetLoops(1, LoopType.Yoyo);
        if (fishCount == GameManager.Instance.strength)
        {
            StopFishing();
        }
    }
}
