
using UnityEngine;

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

    /// <summary>
    /// 儲存生成出來的房子
    /// </summary>
    private GameObject tempHouse;

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
        tempHouse.transform.SetParent(null); // 暫存房子.變形.設定父物件(無)
        tempHouse.GetComponent<Rigidbody>().isKinematic = false; // 暫存房子.取得元件<剛體>().運動學 = false
        Invoke("CreateHouse", 1);
    }
}
