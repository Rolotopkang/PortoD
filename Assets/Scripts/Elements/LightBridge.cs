using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBridge : ReceiverMono
{
    [HideInInspector]
    public float bridgeLength = 3.0f; 
    [HideInInspector]
    public bool tmp_isColliderMode = false;
    
    public Sprite Open;
    public Sprite Close;
    
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    
    
    private void Start()
    {
        Receive(false,null);
    }
    
    
    public override void Receive(bool isTriggered,TriggerMono triggerMono)
    {
        GetComponent<SpriteRenderer>().sprite = isTriggered ? Open : Close;
        GetComponent<BoxCollider2D>().enabled = isTriggered;
    }
    
    public void SetDoorLength(float length , bool isColliderFollow)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        bridgeLength = length;

        // 调整光门精灵的大小
        Vector2 newScale = new Vector2(length, spriteRenderer.size.y);
        spriteRenderer.size = newScale;

        
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
}
