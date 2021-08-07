using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

[SelectionBase]
public class HookController : MonoBehaviour,ITweenable
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
    private int fishCount;

    private void Awake()
    {
        cam = Camera.main;
        collider = GetComponent<CircleCollider2D>();
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
        isHookMoving = true;
        hookButton.gameObject.SetActive(false);
        fishCount = 0;
        camTweener = cam.transform.DOMoveY(hookData.HookLength, hookData.FishingDownwardsTime).OnUpdate(delegate
        {
            if (cam.transform.position.y <= -11f)
            {
                transform.SetParent(cam.transform);
            }
        }).OnComplete(delegate
        {
            collider.enabled = true;
            camTweener = cam.transform.DOMoveY(0, hookData.FishingUpwardsTime).OnUpdate(delegate
             {
                 if (cam.transform.position.y >= -25f)
                 {
                     StopFishing();
                 }
             });
        });
        collider.enabled = false;
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
                 StartCoroutine(WaitFishingRoutine(hookData.WaitTime));
             });
    }

    private IEnumerator WaitFishingRoutine(float time)
    {
        isHookMoving = false;
        yield return new WaitForSecondsRealtime(time);
        hookButton.gameObject.SetActive(true);
        collider.enabled = true;
    }

    public void InitDOTween()
    {
        DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
        DOTween.defaultEaseType = Ease.OutQuart;
    }
}
