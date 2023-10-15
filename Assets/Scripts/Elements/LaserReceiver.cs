using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceiver : TriggerMono , ConvertLaser
{
    public Color TriggeredColor;
    public Color UnTriggeredColor;
    
    public float resetTime = 0.2f;
    private Coroutine ResetBool;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Convert(bool set , Vector3 hitpos)
    {
        if (set)
        {
            if (ResetBool != null)
            {
                StopCoroutine(ResetBool);
                ResetBool = null;
            }
            ResetBool = StartCoroutine(ResetBoolFunc());
        }

        isTriggered = set;
        Trigger(isTriggered);
        
    }

    public override void Trigger(bool isTriggered)
    {
        if (!LinkedGameObject)
        {
            Debug.Log(this.name+"没有绑定接收器");
            return;
        }
        LinkedGameObject.Receive(isTriggered,this);
        _spriteRenderer.color = isTriggered ? TriggeredColor : UnTriggeredColor;
    }

    private IEnumerator ResetBoolFunc()
    {
        yield return new WaitForSeconds(resetTime);
        isTriggered = false;
        Trigger(isTriggered);
    }
}
