using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class CheckPointSystem : Singleton<CheckPointSystem>
{
    public CheckPoint CurrentCheckPoint;

    public bool PlayerDeath;
    
    public void ChangeCheckPoint(CheckPoint checkPoint)
    {
        CurrentCheckPoint = checkPoint;
    }

    public void OnplayerDeath()
    {
        PortalSystem.GetInstance().RefreshAllPortal();
        BoxSystem.GetInstance().ClearAllBox();
        transform.position = CurrentCheckPoint.GetRebornPoint;
        PlayerDeath = false;
    }
}
