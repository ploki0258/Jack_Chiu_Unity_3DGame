using UnityEngine;
using System.Collections; // 引用 系統.集合 API：提供協程所需內容

public class LearnCoroutineAndWhile : MonoBehaviour
{
    // 協同程式方法：注意必須將傳回類型設為 IEnumerator 注意此關鍵字!
    public IEnumerator CoroutineMethod()
    {
        print("嗨，我是第一段");

        // yield 讓
        // yield return new WaitForSeconds(秒數)
        yield return new WaitForSeconds(2);

        // 讓協程等待兩秒再執行下方的敘述
        print("兩秒後會執行");
    }

    /// <summary>
    /// 每個0.5秒生程一隻怪物
    /// </summary>
    /// <param name="enemyCount">要生成的怪物數量</param>
    /// <returns></returns>
    private IEnumerator CreateEnemy(int enemyCount)
    {
        int enemy = 1;                              // 目前怪物編號

        while (enemy <= enemyCount)                 // 當前怪物編號 <= 要生成的怪物數量
        {
            print("生成怪物，編號：" + enemy);       // 輸出怪物編號
            enemy++;                                // 編號遞增
            yield return new WaitForSeconds(0.5f);  // 等待0.5秒
        }
    }

    private void Start()
    {
        StartCoroutine(CoroutineMethod()); // 啟動協程：讓協程開始執行

        int count = 1;  // 區域變數 僅在此區域內允許存取(區域指大括號) 

        // 語法：while (布林值) {敘述：當布林值為 true 時重複執行}
        // 當 count 小於10時執行敘述
        while (count < 10)
        {
            // 輸出當前數字
            print(count);
            // 數字遞增             
            count++;
        }

        // 啟動協程
        StartCoroutine(CreateEnemy(10));
    }
}
