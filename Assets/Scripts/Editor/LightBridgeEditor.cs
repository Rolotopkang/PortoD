// ******************************************************************
//       /\ /|       @file       FILENAME
//       \ V/        @brief      
//       | "")       @author     topkang
//       /  |                    
//      /  \\        @Modified   DATE
//    *(__\_\        @Copyright  Copyright (c) YEAR, TOPGAMING
// ******************************************************************

using UnityEditor;

[CustomEditor(typeof(LightBridge))]
public class LightBridgeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LightBridge lightBridge = (LightBridge)target;

        // 显示默认 Inspector
        DrawDefaultInspector();

        // 添加一个调整长度的滑块
        lightBridge.bridgeLength = EditorGUILayout.Slider("Bridge Length", lightBridge.bridgeLength, 1.0f, 30.0f);
        
        lightBridge.tmp_isColliderMode = EditorGUILayout.Toggle("是否开启碰撞体跟随长度变化", lightBridge.tmp_isColliderMode);
        // 更新光门的长度
        lightBridge.SetDoorLength(lightBridge.bridgeLength, lightBridge.tmp_isColliderMode);
    }
}