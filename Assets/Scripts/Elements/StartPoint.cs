using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class StartPoint : Singleton<StartPoint>
{
    public Vector3 GetStartPoint => transform.GetChild(0).position;
}
