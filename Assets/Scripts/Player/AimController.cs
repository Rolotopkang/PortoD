using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public float weaponAngel;
    private Vector3 mouseScreenPosition = Vector3.zero;
    private Vector3 aimDirection = Vector3.zero;
    
    private Vector3 localScale;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 posScale = new Vector3();
        mouseScreenPosition = Input.mousePosition;
        aimDirection = mouseScreenPosition - mainCamera.WorldToScreenPoint(
            transform.position);
        aimDirection.z = 0;
        aimDirection.Normalize();
        weaponAngel = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        
        //武器翻转
        if (weaponAngel is > 90 or < -90)
        {
            if (transform.localScale.y>0)
            {
                posScale = new Vector3(transform.localScale.x, -transform.localScale.y,
                    transform.localScale.z);
            }
            else
            {
                posScale = new Vector3(transform.localScale.x, transform.localScale.y,
                    transform.localScale.z);
            }
            transform.localScale = posScale;
        }
        else
        {
            if (transform.localScale.y<0)
            {
                posScale = new Vector3(transform.localScale.x, -transform.localScale.y,
                    transform.localScale.z);
            }
            else
            {
                posScale = new Vector3(transform.localScale.x, transform.localScale.y,
                    transform.localScale.z);
            }
            transform.localScale = posScale;
        }
        transform.eulerAngles = new Vector3(0, 0,weaponAngel);
    }
}
