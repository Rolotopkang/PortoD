// ******************************************************************
//       /\ /|       @file       FILENAME
//       \ V/        @brief      
//       | "")       @author     topkang
//       /  |                    
//      /  \\        @Modified   DATE
//    *(__\_\        @Copyright  Copyright (c) YEAR, TOPGAMING
// ******************************************************************


using System;
using System.Collections.Generic;
using Tools;
using UnityEngine.Events;

public class BoxSystem : Singleton<BoxSystem>
{
    public static event Action ONTouchLightWall;

    public void Register(Action action)
    {
        ONTouchLightWall += action ;
    }

    public void UnRegister(Action action)
    {
        ONTouchLightWall -= action;
    }

    public void ClearAllBox()
    {
        if (ONTouchLightWall != null) ONTouchLightWall.Invoke();
    }
}