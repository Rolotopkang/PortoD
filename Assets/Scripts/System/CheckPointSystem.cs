using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class CheckPointSystem : Singleton<CheckPointSystem>
{
    public CheckPoint CurrentCheckPoint;
    public CheckPoint CurrentWaterCheckPoint;

    public bool PlayerDeath;
    
    public void ChangeCheckPoint(CheckPoint checkPoint, bool isWater)
    {
        if (isWater)
        {
            CurrentWaterCheckPoint = checkPoint;
        }
        else
        {
            CurrentCheckPoint = checkPoint;
        }
        
    }

    public void ResetCheckPoints()
    {
        CurrentCheckPoint = null;
        CurrentWaterCheckPoint = null;
    }

    public void OnReloadLevel()
    {
        PortalSystem.GetInstance().ClearAllPortal();
        BoxSystem.GetInstance().ClearAllBox();
        if (CurrentCheckPoint)
        {
            transform.position = CurrentCheckPoint.GetRebornPoint;
        }
        else
        {
            transform.position = StartPoint.GetInstance().GetStartPoint;
        }
    }

    public void OnplayerDeath()
    {
        PortalSystem.GetInstance().ClearAllPortal();
        BoxSystem.GetInstance().ClearAllBox();
        transform.position = CurrentWaterCheckPoint.GetRebornPoint;
        PlayerDeath = false;
    }
}
