using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Rendering;

public class SettingManager : Singleton<SettingManager>
{
    public Animation Animation;
    public AnimationClip open;
    public AnimationClip close;
    public Volume Volume;
    
    private bool isOpen;
    private Coroutine WaitAnim;

    protected override void Awake()
    {
        base.Awake();
        Volume.enabled = false;
        GetComponent<CanvasGroup>().interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                HideUI();
            }
            else
            {
                ShowUI();
            }
        }
    }

    public void ReturnLobby()
    {
        HideUI();
        SceneTransitionSystem.GetInstance().ChangeScene(1);
    }

    public void Reload()
    {
        HideUI();
        CheckPointSystem.GetInstance().OnReloadLevel();
    }

    public void QuitGame()
    {
        HideUI();
        Application.Quit();
    }

    private void ShowUI()
    {
        if(Animation.isPlaying) return;
        Animation.clip = open;
        Animation.Play();
        if (WaitAnim != null)
        {
            StopCoroutine(WaitAnim);
            WaitAnim = null;
        }
        WaitAnim = StartCoroutine(WaitAnimation(true));
    }

    private void HideUI()
    {
        if(Animation.isPlaying) return;
        Animation.clip = close;
        Time.timeScale = 1;
        Volume.enabled = false;
        GetComponent<CanvasGroup>().interactable = false;
        Animation.Play();
        if (WaitAnim != null)
        {
            StopCoroutine(WaitAnim);
            WaitAnim = null;
        }
        WaitAnim = StartCoroutine(WaitAnimation(false));
    }

    private IEnumerator WaitAnimation(bool show)
    {
        yield return new WaitUntil(() =>!Animation.isPlaying);
        if (show)
        {
            isOpen = true;
            Time.timeScale = 0;
            Volume.enabled = true;
            GetComponent<CanvasGroup>().interactable = true;
        }
        else
        {
            isOpen = false;
        }
    }
}
