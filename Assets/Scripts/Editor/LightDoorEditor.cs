// ******************************************************************
//       /\ /|       @file       FILENAME
//       \ V/        @brief      
//       | "")       @author     topkang
//       /  |                    
//      /  \\        @Modified   DATE
//    *(__\_\        @Copyright  Copyright (c) YEAR, TOPGAMING
// ******************************************************************


using UnityEditor;

[CustomEditor(typeof(LightDoor))]
public class LightDoorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LightDoor lightDoor = (LightDoor)target;

        // 显示默认 Inspector
        DrawDefaultInspector();
        
        // 添加一个调整长度的滑块
        lightDoor.bridgeLength = EditorGUILayout.Slider("Bridge Length", lightDoor.bridgeLength, 1.0f, 30.0f);
        
        lightDoor.tmp_isColliderMode = EditorGUILayout.Toggle("是否开启碰撞体跟随长度变化", lightDoor.tmp_isColliderMode);
        // 更新光门的长度
        lightDoor.SetDoorLength(lightDoor.bridgeLength, lightDoor.tmp_isColliderMode);
    }
}
