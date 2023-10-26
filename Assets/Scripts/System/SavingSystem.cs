using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class SavingSystem : Singleton<SavingSystem>
{
    public bool ifReset;
    protected override void Awake()
    {
        base.Awake();
        if (!PlayerPrefs.HasKey("Level1"))
        {
            PlayerPrefs.SetInt("Level1",1);
        }
        if (!PlayerPrefs.HasKey("Level2"))
        {
            PlayerPrefs.SetInt("Level2",1);
        }
        if (!PlayerPrefs.HasKey("Level3"))
        {
            PlayerPrefs.SetInt("Level3",1);
        }
        if (!PlayerPrefs.HasKey("Level4"))
        {
            PlayerPrefs.SetInt("Level4",1);
        }
    }

    private void Start()
    {
        if (ifReset)
        {
            PlayerPrefs.SetInt("Level1",1);
            PlayerPrefs.SetInt("Level2",1);
            PlayerPrefs.SetInt("Level3",1);
            PlayerPrefs.SetInt("Level4",1);
        }
    }

    /// <summary>
    /// 设置关卡解锁情况
    /// </summary>
    /// <param name="level"></param>
    /// <param name="locked"></param>
    public void SetLevelCondition(int level, bool locked)
    {
        switch (level)
        {
            case 0:
                break;
            case 1:
                PlayerPrefs.SetInt("Level1",locked? 1: 0);
                break;
            case 2:
                PlayerPrefs.SetInt("Level2",locked? 1: 0);
                break;
            case 3:
                PlayerPrefs.SetInt("Level3",locked? 1: 0);
                break;
            case 4:
                PlayerPrefs.SetInt("Level4",locked? 1: 0);
                break;
        }
    }

    public bool GetLevelCondition(int level)
    {
        switch (level)
        {
            case 0:
                return false;
            case 1:
                return PlayerPrefs.GetInt("Level1")==1;
            case 2:
                return PlayerPrefs.GetInt("Level2")==1;
            case 3:
                return PlayerPrefs.GetInt("Level3")==1;
            case 4:
                return PlayerPrefs.GetInt("Level4")==1;
        }

        return true;
    }
}
