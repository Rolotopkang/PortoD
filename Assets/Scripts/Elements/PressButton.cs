using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : TriggerMono
{
    public Sprite Pressed;
    public Sprite UnPressed;
    public float ResetTime = 0.5f;

    private bool CanBePressed= false;
    private bool CoolingDown = false;
    private Coroutine ResetButton;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = UnPressed;
        ShowHint(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShowHint(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShowHint(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanBePressed && !CoolingDown)
        {
            isTriggered = true;
            Trigger(true);
        }
    }

    private void ShowHint(bool set)
    {
        transform.GetChild(0).gameObject.SetActive(set);
        CanBePressed = set;
    }

    public override void Trigger(bool isTriggered)
    {
        LinkedGameObject.Receive(isTriggered);
        GetComponent<SpriteRenderer>().sprite = Pressed;
        CoolingDown = true;
        
        
        if (ResetButton != null)
        {
            StopCoroutine(ResetButton);
            ResetButton = null;
        }
        ResetButton = StartCoroutine(StartResetButton());
    }

    private IEnumerator StartResetButton()
    {
        yield return new WaitForSeconds(ResetTime);
        CoolingDown = false;
        isTriggered = false;
        LinkedGameObject.Receive(isTriggered);
        GetComponent<SpriteRenderer>().sprite = UnPressed;
 
    }
}
