using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public GameObject BlueBulletPrefab;
    public GameObject OrangeBulletPrefab;
    public GameObject Muzzle;
    public float speed = 10;
    public float shootingspeed = 1;


    private bool isBluePortal;
    private float useTimer;
    private void Update()
    {
        if (!PlayerController.GetInstance().isHolding)
        {
            KeyDown();
        }
    }
    
    private void KeyDown()
    {
        if (Input.GetMouseButton(0))
        {
            isBluePortal = true;
            ShootTimer();
        }else if (Input.GetMouseButton(1))
        {
            isBluePortal = false;
            ShootTimer();
        }
        else
        {
            ReTimer();
        }
    }
    
    private void ShootTimer()
    {
        if(!(useTimer>=shootingspeed)) useTimer += Time.deltaTime;
        //射击
        if (useTimer >= shootingspeed)
        { 
            Shoot(); 
            useTimer = 0;
        }
    }
    
    private void ReTimer()
    {
        if(useTimer<shootingspeed)
            useTimer += Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject tmp_bullet = Instantiate(isBluePortal? BlueBulletPrefab : OrangeBulletPrefab, Muzzle.transform.position, Muzzle.transform.rotation);
        // Rigidbody2D rb = tmp_bullet.GetComponent<Rigidbody2D>();
        // rb.velocity = Muzzle.transform.right * speed;
    }

}
