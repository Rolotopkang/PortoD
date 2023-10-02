// ******************************************************************
//       /\ /|       @file       FILENAME
//       \ V/        @brief      
//       | "")       @author     topkang
//       /  |                    
//      /  \\        @Modified   DATE
//    *(__\_\        @Copyright  Copyright (c) YEAR, TOPGAMING
// ******************************************************************


using Tools;
using UnityEditor;

[CustomEditor(typeof(BoxGenerator))]
public class BoxGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        BoxGenerator boxGenerator = (BoxGenerator)target;
        
        // 显示默认 Inspector
        DrawDefaultInspector();
        
        EditorGUI.BeginChangeCheck();
        boxGenerator.BoxColor = (EnumTool.BoxColor)EditorGUILayout.EnumPopup("触发器颜色", boxGenerator.BoxColor);
        if (EditorGUI.EndChangeCheck())
        {
            // 当Enum属性发生变化时调用方法
            boxGenerator.ChangeColor();
        }
        serializedObject.ApplyModifiedProperties();
    }
}