// ******************************************************************
//       /\ /|       @file       FILENAME
//       \ V/        @brief      
//       | "")       @author     topkang
//       /  |                    
//      /  \\        @Modified   DATE
//    *(__\_\        @Copyright  Copyright (c) YEAR, TOPGAMING
// ******************************************************************


using System;
using System.Collections.Generic;
using UnityEngine;

public class LightDoor : ReceiverMono
{
    [HideInInspector]
    public float bridgeLength = 3.0f; 
    [HideInInspector]
    public bool tmp_isColliderMode = false;

    public int ReceiverNum = 1;

    [SerializeField] private List<TriggerMono> _triggerMonos = new List<TriggerMono>();
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        RefreshSprite();
    }
    
    
    public override void Receive(bool isTriggered, TriggerMono triggerMono)
    {
        if (isTriggered)
        {
            if (!_triggerMonos.Contains(triggerMono))
            {
                _triggerMonos.Add(triggerMono);
            }
        }
        else
        {
            if (_triggerMonos.Contains(triggerMono))
            {
                _triggerMonos.Remove(triggerMono);
            }
        }
        RefreshSprite();
    }

    public void RefreshSprite()
    {
        switch (ReceiverNum)
        {
            case 1:
                switch (GetCurrentTriggerNum)
                {
                    case 0:
                        spriteRenderer.enabled = true;
                        transform.GetChild(0).gameObject.SetActive(false);
                        boxCollider.enabled = true;
                        break;
                    case 1:
                        spriteRenderer.enabled = false;
                        transform.GetChild(0).gameObject.SetActive(true);
                        boxCollider.enabled = false;
                        break;
                }
                break;
            case 2:
                switch (GetCurrentTriggerNum)
                {
                    case 0:
                        GetComponent<SpriteRenderer>().enabled = true;
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.SetActive(false);
                        boxCollider.enabled = true;
                        break;
                    case 1:
                        GetComponent<SpriteRenderer>().enabled = true;
                        transform.GetChild(0).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.SetActive(false);
                        boxCollider.enabled = true;
                        break;
                    case 2:
                        GetComponent<SpriteRenderer>().enabled = false;
                        transform.GetChild(0).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.SetActive(true);
                        boxCollider.enabled = false;
                        break;
                }
                break;
            case 3:
                switch (GetCurrentTriggerNum)
                {
                    case 0:
                        GetComponent<SpriteRenderer>().enabled = true;
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(false);
                        boxCollider.enabled = true;
                        break;
                    case 1:
                        GetComponent<SpriteRenderer>().enabled = true;
                        transform.GetChild(0).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(false);
                        boxCollider.enabled = true;
                        break;
                    case 2:
                        GetComponent<SpriteRenderer>().enabled = true;
                        transform.GetChild(0).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.SetActive(true);
                        transform.GetChild(2).gameObject.SetActive(false);
                        boxCollider.enabled = true;
                        break;
                    case 3:
                        GetComponent<SpriteRenderer>().enabled = false;
                        transform.GetChild(0).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.SetActive(true);
                        transform.GetChild(2).gameObject.SetActive(true);
                        boxCollider.enabled = false;
                        break;
                }
                break;
        }
    }
    
    public void SetDoorLength(float length , bool isColliderFollow)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        bridgeLength = length;

        // 调整光门精灵的大小
        Vector2 newScale = new Vector2(length, spriteRenderer.size.y);
        spriteRenderer.size = newScale;

        foreach (SpriteRenderer Renderer in spriteRenderers)
        {
            newScale = new Vector2(length, Renderer.size.y);
            Renderer.size = newScale;
        }

        
        if (isColliderFollow)
        {
            // 调整碰撞箱的大小
            Vector2 newSize = new Vector2( length,boxCollider.size.y);
            boxCollider.size = newSize;

            // // 更新碰撞箱的偏移以保持位置不变
            // float offsetX = (length - 1.0f) / 2.0f; // 如果光门的锚点在中间，偏移应该是0.5f
            // Vector2 newOffset = new Vector2(boxCollider.offset.x, offsetX);
            // boxCollider.offset = newOffset;
        }
    }

    public int GetCurrentTriggerNum => _triggerMonos.Count;
}
