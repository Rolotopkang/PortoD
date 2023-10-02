using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PressurePlate : TriggerMono
{
    public SpriteRenderer plate;
    public SpriteRenderer Base;
    public Color UnpressedColor;
    public Color PressedColor;


    private bool isPressed = false;
    private int NumInCollider2D = 0;

    private void Start()
    {
        UnPressed();
    }

    private void Update()
    {
        if (isPressed)
        {
            Pressed();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Player"))
        {
            if (NumInCollider2D == 0)
            {
                isPressed = true;
            }

            NumInCollider2D++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Player"))
        {
            if (NumInCollider2D == 1)
            {
                isPressed = false;
                UnPressed();
            }
            NumInCollider2D--;
        }
    }


    public override void Trigger(bool isTriggered)
    {
        if (LinkedGameObject)
        {
            LinkedGameObject.Receive(isTriggered);
        }
        else
        {
            Debug.LogWarning("触发器"+transform.name+"没有绑定！");
        }

    }

    public void Pressed()
    {
        plate.gameObject.SetActive(false);
        Base.color = PressedColor;
        isTriggered = true;
        Trigger(isTriggered);
    }

    public void UnPressed()
    {
        plate.gameObject.SetActive(true);
        Base.color = UnpressedColor;
        isTriggered = false;
        Trigger(isTriggered);
    }
}
