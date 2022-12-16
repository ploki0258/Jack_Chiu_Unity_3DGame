using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public GameObject obj = null;
    
    // 獲取組件
    private ScrollRect Rect;
    // 紀錄圖片相應的位置
    private float[] posArray = new float[] { 360f, 1080f, 1800f };
    // 設置滾動的目標位置
    private float targetPos;
    // 判斷是否正在拖拽
    private bool isDrag = false;
    private int index = 0;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;

        // 獲取當前的位置
        Vector2 pos = Rect.normalizedPosition;
        // 保留與第一張圖片的位置
        float x = Mathf.Abs(pos.x - posArray[0]);

        for (int i = 0; i < 3; i++)
        {
            float temp = Mathf.Abs(pos.x - posArray[i]);
            if (temp < x)
            {
                x = temp;
                index = i;
            }
        }

        targetPos = posArray[index];
    }

    private void Start()
    {
        Rect = GetComponent<ScrollRect>();
        // contents = transform.Find("Viewport/Content") as RectTransform;

        // delta_x = Mathf.CeilToInt(count / 3) * 720f;
        // contents.sizeDelta = new Vector2(delta_x, contents.sizeDelta.y);
    }

    private void Update()
    {
        if (isDrag == true)
        {
            Rect.horizontalNormalizedPosition = Mathf.Lerp(Rect.horizontalNormalizedPosition,targetPos,Time.deltaTime * 4);
        }
    }

}
