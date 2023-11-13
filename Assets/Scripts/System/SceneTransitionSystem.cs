using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionSystem : Singleton<SceneTransitionSystem>
{
    public Animation Animation;
    public AnimationClip Show;
    public AnimationClip Hide;
    public bool IsChanging;
    public bool isDebug;
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }

    public void onSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (Animation)
        {
            Animation.clip = Show;
            Animation.Play();
        }

        IsChanging = false;
        
        if (isDebug)
        {
            isDebug = false; return; }
        PlayerController.GetInstance().transform.position = StartPoint.GetInstance().GetStartPoint;
    }

    public void ChangeScene(int SceneNum)
    {
        if(IsChanging) return;
        Animation.clip = Hide;
        Animation.Play();
        IsChanging = true;
        CheckPointSystem.GetInstance().ResetCheckPoints();
        StartCoroutine(CS(SceneNum));
    }

    private IEnumerator CS(int n)
    {
        yield return new WaitUntil(()=> !Animation.isPlaying);
        SceneManager.LoadSceneAsync(n);
    }
}
