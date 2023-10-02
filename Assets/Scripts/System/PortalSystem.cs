using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class PortalSystem : Singleton<PortalSystem>
{
    public Portal CurrentBluePortal;
    public Portal CurrentOrangePortal;
    [Range(0,1)]
    public float PortalSystemAlpha = 1;

    public float MinVelocity = 3f;

    public void PortalRegister(Portal portal)
    {
        if (portal.PortalGunBulletType == EnumTool.PortalGunBulletType.Bule)
        {
            if (CurrentBluePortal!= null)
            {
                CurrentBluePortal.DestroySelf();
            }
            CurrentBluePortal = portal;
        }else if (portal.PortalGunBulletType == EnumTool.PortalGunBulletType.Orange)
        {
            if (CurrentOrangePortal!= null)
            {
                CurrentOrangePortal.DestroySelf();
            }
            CurrentOrangePortal = portal;
        }
        RefreshAllPortal();
    }


    public void TP(GameObject gameObject, EnumTool.PortalGunBulletType type)
    {
        if (isConnectionBuilt())
        {
            if (type == EnumTool.PortalGunBulletType.Bule)
            {
                // Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity * -CurrentBluePortal.gameObject.transform.up);
                // Debug.Log("进入传送门向量速度" + (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude *
                //                          -CurrentBluePortal.gameObject.transform.up).magnitude);
                // Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity);
                // Debug.Log(-CurrentBluePortal.gameObject.transform.up);
                // Debug.Log(CurrentOrangePortal.gameObject.transform.up *
                //           (gameObject.GetComponent<Rigidbody2D>().velocity *
                //            -CurrentBluePortal.gameObject.transform.up)
                //           .magnitude);
                
                gameObject.transform.position = CurrentOrangePortal.transform.position;
                gameObject.GetComponent<Rigidbody2D>().velocity = CurrentOrangePortal.gameObject.transform.up *
                                                                  (gameObject.GetComponent<Rigidbody2D>().velocity *
                                                                   -CurrentBluePortal.gameObject.transform.up)
                                                                  .magnitude * ((gameObject.GetComponent<Rigidbody2D>().velocity *
                                                                      -CurrentBluePortal.gameObject.transform.up)
                                                                  .magnitude>MinVelocity? PortalSystemAlpha : 1 );
  
            }
            else
            {
                // Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity * -CurrentOrangePortal.gameObject.transform.up);  
                gameObject.transform.position = CurrentBluePortal.transform.position;
                gameObject.GetComponent<Rigidbody2D>().velocity = CurrentBluePortal.gameObject.transform.up *
                                                                  (gameObject.GetComponent<Rigidbody2D>().velocity *
                                                                   -CurrentOrangePortal.gameObject.transform.up)
                                                                  .magnitude * ((gameObject.GetComponent<Rigidbody2D>().velocity *
                                                                          -CurrentOrangePortal.gameObject.transform.up)
                                                                      .magnitude>MinVelocity? PortalSystemAlpha : 1 );
            }

        }
    }


    public void RefreshAllPortal()
    {
        if (CurrentBluePortal != null)
        {
            CurrentBluePortal.RefreshSprite();
        }
        if (CurrentOrangePortal != null)
        {
            CurrentOrangePortal.RefreshSprite();
        }
    }
    
    public void ClearAllPortal()
    {
        if (CurrentBluePortal != null)
        {
            CurrentBluePortal.DestroySelf();
            CurrentBluePortal = null;
        }
        if (CurrentOrangePortal != null)
        {
            CurrentOrangePortal.DestroySelf();
            CurrentOrangePortal = null;
        }
    }
    
    public bool isConnectionBuilt() => CurrentBluePortal != null && CurrentOrangePortal != null;
    
}
