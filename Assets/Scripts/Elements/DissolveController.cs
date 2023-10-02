using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DissolveController : MonoBehaviour
{
    [SerializeField] 
    private float dissolveSpeed = 1f;
    
    
    private float fade = 1;
    private Material dissolve;
    private bool isDissolving = false;
    private UnityAction OnDissolveFinished;
    
    private void Start()
    {
        dissolve = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if(isDissolving) DissolveHide();
    }

    public void DissolveHide()
    {
        fade -= Time.deltaTime* dissolveSpeed;
        Debug.Log(fade);
        if (fade <= 0f)
        {
            fade = 0f;
            isDissolving = false;
            OnDissolveFinished.Invoke();
            OnDissolveFinished = null;
        }
        dissolve.SetFloat("_Fade",fade);
    }

    public void Hide(UnityAction action)
    {
        fade = 1;
        isDissolving = true;
        dissolve.SetFloat("_Fade",1);
        OnDissolveFinished += action;
    }
}
