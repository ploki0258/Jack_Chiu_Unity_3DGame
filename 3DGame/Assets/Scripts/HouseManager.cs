using UnityEngine;
using UnityEngine.UI;

public class HouseManager : MonoBehaviour
{
    // Transform 可以儲存物件的 Transform 元件.可以取得座標、角度或尺寸資訊
    // Rigidbody 可以儲存物件的 Rigidbody 元件.可以取得物理資訊
    // GameObject 可以儲存預製物或場景上的物件

    [Header("懸吊房子物件")]
    public Transform pointSuspention;
    [Header("晃動位置")]
    public Transform pointShake;
    [Header("晃動位置剛體")]
    public Rigidbody pointShakeRig;
    [Header("房子欲置物陣列")]
    public GameObject[] houses;
    [Header("晃動力道"), Range(0.5f, 10f)]
    public float shakePower = 2f;
    [Header("晃動頻率"), Range(1, 8)]
    public int shakefreq = 3;
    [Header("攝影機")]
    public Transform myCamera;
    [Header("檢查遊戲失敗")]
    public Transform checkWall;
    [Header("遊戲結算")]
    public GameObject final;
    [Header("蓋房子數量文字介面")]
    public Text textHouseCount;
    [Header("最佳數量文字介面")]
    public Text textBest;
    [Header("本次數量文字介面")]
    public Text textCurrent;

    /// <summary>
    /// 儲存生成出來的房子
    /// </summary>
    private GameObject tempHouse;

    /// <summary>
    /// 開始蓋房子
    /// </summary>
    private bool startHourse;

    /// <summary>
    /// 房子總高度
    /// </summary>
    private float height;

    /// <summary>
    /// 第一個房子
    /// </summary>
    private Transform firstHouse;

    /// <summary>
    /// 房子總數
    /// </summary>
    private int count;

    private void Start()
    {
        CreateHouse(); // 呼叫生成房子函式
        InvokeRepeating("Shake", 0, shakefreq); // 重複調用函式("函式名稱",開始時間,重複頻率)
    }

    /// <summary>
    /// 建立房子
    /// </summary>
    private void CreateHouse()
    {
        tempHouse = Instantiate(houses[0], pointShake);
    }

    /// <summary>
    /// 晃動房子
    /// </summary>
    private void Shake()
    {
        // 晃動位置剛體 = 晃動剛體.右邊 * 力道
        pointShakeRig.velocity = pointShake.right * shakePower;
    }

    /// <summary>
    /// 蓋房子
    /// </summary>
    public void HouseDown()
    {
        tempHouse.transform.SetParent(null);                     // 暫存房子.變形.設定父物件(無)
        tempHouse.GetComponent<Rigidbody>().isKinematic = false; // 暫存房子.取得元件<剛體>().運動學 = false
        tempHouse.GetComponent<House>().down = true;             // 暫存房子.取得元件<房子>().是否掉落中 = true
        Invoke("CreateHouse", 1); // 延遲調用函式("函式名稱", 延遲時間)
        startHourse = true; // 開始蓋房子
        // 房子總高度 遞增指定 暫存房子.取得元件<盒形碰撞器>().尺寸.y * 房子尺寸.y
        //有些房子有縮放，例如大房子縮小到0.7 所以實際尺寸為碰撞器 * 尺寸
        height += tempHouse.GetComponent<BoxCollider>().size.y * tempHouse.transform.localScale.y;

        // 如果 還沒有第一個房子
        if (!firstHouse)
        {
            firstHouse = tempHouse.transform;   // 第一個房子 = 暫存房子.變形
            Invoke("CreateCheckWall", 1.2f);    // 延遲調用函式(建立遊戲失敗牆壁, 1.2秒)
            Destroy(firstHouse.GetComponent<House>());  // 刪除(第一個房子.取得元件<房子>())
        }

        count++;                                    // 房子總數遞增
        textHouseCount.text = "房子總數：" + count;  // 蓋房子數量文字介面.文字 = "房子總數：" + 房子總數
    }

    private void Update()
    {
        Track();
    }

    /// <summary>
    /// 攝影機追蹤
    /// 懸吊房子物件位移
    /// </summary>
    private void Track()
    {
        // 如果(開始蓋房子)
        if (startHourse)
        {
            // 攝影機新座標 = (0, 房子總高度, -10)
            Vector3 posCam = new Vector3(0, height, -10);
            // 攝影機.座標 = 三維向量.插植(攝影機.座標, 攝影機新座標, 0.3 (百分比) * 速度 * 一個影格時間)
            myCamera.position = Vector3.Lerp(myCamera.position, posCam, 0.3f * 10 * Time.deltaTime);

            Vector3 posSus = new Vector3(0, height + 6, 0);

            pointSuspention.position = Vector3.Lerp(pointSuspention.position, posSus, 0.3f * 10 * Time.deltaTime);

        }
    }

    /// <summary>
    /// 建立遊戲失敗牆壁
    /// </summary>
    private void CreateCheckWall()
    {
        // 生成(檢查遊戲失敗牆壁, 第一個房子.座標, 零角度)
        Instantiate(checkWall, firstHouse.position, Quaternion.identity);
    }

    /// <summary>
    /// 遊戲結束：顯示結算畫面
    /// </summary>
    public void GameOver()
    {
        final.SetActive(true);

        textCurrent.text = "本次總數：" + count;

        if (count > PlayerPrefs.GetInt("最佳數量"))
            PlayerPrefs.SetInt("最佳數量", count);

        textBest.text = "最佳數量：" + PlayerPrefs.GetInt("最佳數量", count);
    }
}
