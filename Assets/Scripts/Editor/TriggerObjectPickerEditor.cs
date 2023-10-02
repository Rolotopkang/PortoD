using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerMono),true)]
public class TriggerObjectPickerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // 默认的检查器面板
        if (GUILayout.Button("吸管选取GameObject"))
        {
            // 启用吸管模式
            SceneView.duringSceneGui += PickGameObjectWithSipper;
        }
    }

    private void PickGameObjectWithSipper(SceneView sceneView)
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            // Debug.Log("点击！");
            //
            // // 射线检测场景中的GameObject
            // Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            // RaycastHit hitInfo;
            
            GameObject pickedObject = HandleUtility.PickGameObject(Event.current.mousePosition, true);
            
            if (pickedObject && pickedObject.GetComponent<ReceiverMono>())
            {
                
                TriggerMono triggerMono = (TriggerMono)target;
                triggerMono.LinkedGameObject = pickedObject.GetComponent<ReceiverMono>();
 
                // 关闭吸管模式
                SceneView.duringSceneGui -= PickGameObjectWithSipper;
                // 更新检查器
                Repaint();
            }
        }
    }
}
