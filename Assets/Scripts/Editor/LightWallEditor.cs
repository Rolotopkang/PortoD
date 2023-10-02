// ******************************************************************
//       /\ /|       @file       FILENAME
//       \ V/        @brief      
//       | "")       @author     topkang
//       /  |                    
//      /  \\        @Modified   DATE
//    *(__\_\        @Copyright  Copyright (c) YEAR, TOPGAMING
// ******************************************************************

using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(LightWall))]
public class LightWallEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LightWall lightWall = (LightWall)target;

        // 显示默认 Inspector
        DrawDefaultInspector();

        // 添加一个调整长度的滑块
        lightWall.wallLength = EditorGUILayout.Slider("Door Length", lightWall.wallLength, 1.0f, 10.0f);
        
        lightWall.tmp_isColliderMode = EditorGUILayout.Toggle("是否开启碰撞体跟随长度变化", lightWall.tmp_isColliderMode);
        // 更新光门的长度
        lightWall.SetDoorLength(lightWall.wallLength, lightWall.tmp_isColliderMode);
    }
}