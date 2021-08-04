using UnityEngine;
using DG.Tweening;

[SelectionBase]
public class HookController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] GameObject hookedObject;
    [SerializeField] bool canMove;
    [SerializeField] int length;
    [SerializeField] int strength;

    [Header("Normal")]
    [Range(1f, 10f)]
    [SerializeField] float fishingDownwardsTime;
    [Range(1f, 10f)]
    [SerializeField] float fishingUpwardsTime;
    [Range(0f, 1f)]
    [SerializeField] float fishingUpwardsBurstTime;

    private new CircleCollider2D collider;
    private Tweener camTweener;
    private Camera cam;
    private Vector3 startPos;
    private int fishCount;


    private void Awake()
    {
        //Camera Initialization
        cam = Camera.main;
        collider = GetComponent<CircleCollider2D>();

        //DOTween Initialization
        DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
        DOTween.defaultEaseType = Ease.Linear;
    }

    private void Start()
    {
        canMove = true;
        collider.enabled = false;
        startPos = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (canMove && Input.GetMouseButton(0))
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
        }
    }

    public void StartFishing()
    {
        length = -50;
        strength = 3;
        fishCount = 0;
        camTweener = cam.transform.DOMoveY(length, fishingDownwardsTime).OnUpdate(delegate
        {
            if (cam.transform.position.y <= -11f)
            {
                transform.SetParent(cam.transform);
            }
        }).OnComplete(delegate
        {
            collider.enabled = true;
            camTweener = cam.transform.DOMoveY(0, fishingUpwardsTime).OnUpdate(delegate
             {
                 if (cam.transform.position.y >= -25f)
                 {
                     StopFishing();
                 }
             });
        });
        collider.enabled = false;
        canMove = true;
    }

    private void StopFishing()
    {
        canMove = false;
        camTweener.Kill(false);
        camTweener = cam.transform.DOMoveY(0, fishingUpwardsBurstTime).OnUpdate(delegate
             {
                 if (cam.transform.position.y >= -11)
                 {
                     transform.SetParent(null);
                     transform.position = startPos;
                 }
             }).OnComplete(delegate
             {
                 collider.enabled = true;
             });
    }
}
