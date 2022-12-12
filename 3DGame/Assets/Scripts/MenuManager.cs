using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
    public GameObject objBtn;

    private RectTransform contents;
    private ScrollRect scrollRect;

    private float taragePoint;
    private int count = 12;
    private float delta_x;
    private bool isRoll;

    private void Start()
    {
        contents = transform.Find("Viewport/Content") as RectTransform;
        scrollRect = transform.GetComponent<ScrollRect>();

        delta_x = Mathf.CeilToInt(count / 3) * 720f;
        contents.sizeDelta = new Vector2(delta_x, contents.sizeDelta.y);
    }

    private void leftBtn()
    {
        
    }
}
