
using UnityEngine;

public class LearnMethod : MonoBehaviour
{
    // 事件：開始 - 遊戲開始時執行一次
    private void Start()
    {
        MyMeyhod(); // 呼叫函式
        HelloPeople("小白");
        walk("右邊",60);

        Shoot();
        Shoot("炸彈");
    }

    // 修飾詞 傳回類型 函式名稱 (參數) {敘述}
    public void MyMeyhod()
    {
        Debug.Log("Hi 我是函式~");
    }

    public void Hello()
    {
        Debug.Log("哈囉~");
    }


    public void HelloPeople(string people)
    {
        Debug.Log("哈囉~ " + people);
    }

    public void walk(string direction, int speed)
    {
        print("走路方向：" + direction);
        print("走路速度：" + speed);
    }

    /// <summary>
    /// 發射子彈物件
    /// </summary>
    public void Shoot()
    {
        print("發射：子彈");
    }

    /// <summary>
    /// 發射指定物件
    /// </summary>
    /// <param name="prop">指定物件</param>
    public void Shoot(string prop)
    {
        print("發射：" + prop);
    }
}
