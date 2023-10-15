using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReceiverMono : MonoBehaviour
{
    public abstract void Receive(bool isTriggered , TriggerMono triggerMono);
    
}
