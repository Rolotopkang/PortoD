using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHint : MonoBehaviour
{
    private Transform parentTransform; // 物品的Transform
    private Box Box;

    void Start()
    {
        parentTransform = transform.parent; // 获取物品的Transform
        Box = parentTransform.GetComponent<Box>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<PlayerController>().isHolding)
        {
           Box.ShowHint(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Box.ShowHint(false);
        }
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // 获取物品的旋转
        Quaternion itemRotation = parentTransform.rotation;
        
        // 计算提示图标的旋转，使其与物品相反
        Quaternion oppositeRotation = Quaternion.Inverse(itemRotation) ;
        
        
        // 应用相反的旋转到提示图标
        transform.localRotation = oppositeRotation;
    }

    void LateUpdate()
    {
        
    }
}
