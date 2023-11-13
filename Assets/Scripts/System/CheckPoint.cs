using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckPoint : MonoBehaviour
{
    public bool isWater = false;
    public Vector3 GetRebornPoint => transform.GetChild(0).position;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckPointSystem.GetInstance().ChangeCheckPoint(this, isWater);
        }
    }
}
