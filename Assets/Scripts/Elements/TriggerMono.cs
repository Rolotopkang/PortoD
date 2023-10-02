using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerMono : MonoBehaviour
{
    public bool isTriggered;
    
    public ReceiverMono LinkedGameObject;
    
    public abstract void Trigger(bool isTriggered);
}
