using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFriedChicken : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 2) == 1)
        {
            Debug.Log("吃炸鸡");
        }
        else
        {
            Debug.Log("不吃炸鸡");
        }
    }
}
