using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWall : ReceiverMono
{
    [HideInInspector]
    public float wallLength = 3.0f; // 初始光门长度
    [HideInInspector]
    public bool tmp_isColliderMode = false;

    public Sprite Open;
    public Sprite Close;
    
    
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Close;
    }

    public override void Receive(bool isTriggered)
    {
        GetComponent<SpriteRenderer>().sprite = isTriggered ? Open : Close;
        GetComponent<BoxCollider2D>().enabled = !isTriggered;
    }
    
    // 设置光门的长度和碰撞箱的大小
    public void SetDoorLength(float length , bool isColliderFollow)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        wallLength = length;

        // 调整光门精灵的大小
        Vector2 newScale = new Vector2(spriteRenderer.size.x, length);
        spriteRenderer.size = newScale;

        
        if (isColliderFollow)
        {
            // 调整碰撞箱的大小
            Vector2 newSize = new Vector2(boxCollider.size.x, length);
            boxCollider.size = newSize;

            // // 更新碰撞箱的偏移以保持位置不变
            // float offsetX = (length - 1.0f) / 2.0f; // 如果光门的锚点在中间，偏移应该是0.5f
            // Vector2 newOffset = new Vector2(boxCollider.offset.x, offsetX);
            // boxCollider.offset = newOffset;
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PortalSystem.GetInstance().ClearAllPortal();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PortalSystem.GetInstance().ClearAllPortal();
        }
    }
}
