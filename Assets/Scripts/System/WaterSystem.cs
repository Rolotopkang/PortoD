using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!CheckPointSystem.GetInstance().PlayerDeath)
            {
                CheckPointSystem.GetInstance().PlayerDeath = true;
                Invoke("Onplayerdeath",1f);
            }
        }

        if (other.gameObject.CompareTag("Box"))
        {
            other.gameObject.GetComponent<Box>().DestroySelf();
        }
    }

    private void Onplayerdeath()
    {
        CheckPointSystem.GetInstance().OnplayerDeath();
    }
}
