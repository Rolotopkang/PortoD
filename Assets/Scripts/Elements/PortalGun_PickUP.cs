using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun_PickUP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.GetInstance().GetPortalGun();
            Destroy(gameObject);
        }
    }
}
