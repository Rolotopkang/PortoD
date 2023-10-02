using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    public float selfDestructTime = 5f; // 自我销毁的时间（秒）

    private void Start()
    {
        // 启动协程，在一定时间后执行销毁操作
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        // 等待一定的时间
        yield return new WaitForSeconds(selfDestructTime);

        // 销毁物体
        Destroy(gameObject);
    }
}
