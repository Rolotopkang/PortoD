// ******************************************************************
//       /\ /|       @file       FILENAME
//       \ V/        @brief      
//       | "")       @author     topkang
//       /  |                    
//      /  \\        @Modified   DATE
//    *(__\_\        @Copyright  Copyright (c) YEAR, TOPGAMING
// ******************************************************************


using System;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public int SceneNum;
    public bool CheckLock;
    public BoxCollider2D BoxCollider2D;
    public int UnlockLevel = 0;

    private void Start()
    {
        if (CheckLock)
        {
           SetLock(SavingSystem.GetInstance().GetLevelCondition(SceneNum - 1));  
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SavingSystem.GetInstance().SetLevelCondition(UnlockLevel, false);
            SceneTransitionSystem.GetInstance().ChangeScene(SceneNum);
        }
    }

    private void SetLock(bool locked)
    {
        BoxCollider2D.enabled = !locked;
        transform.GetChild(0).gameObject.SetActive(!locked);
    }
}
