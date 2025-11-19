using Common.Data;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTool

{
    [MenuItem("Map Tool/Export Teleporters")]
    public static void ExporTeleporters()
    {
        DataManager.Instance.Load();

        Scene current = EditorSceneManager.GetActiveScene();
        string currentScene = current.name;
        if (current.isDirty)
        {
            EditorUtility.DisplayDialog("提示", "请先保存当前场景", "确定");
            return;
        }
        List<TeleporterObject> allTeleporters = new List<TeleporterObject>();

        foreach (var map in DataManager.Instance.Maps)
        {
            string sceneFile = "Assets/Levels/" + map.Value.Resource + ".unity";
            if (!System.IO.File.Exists(sceneFile))
            {
                Debug.LogWarningFormat("Scene {0} not existed!", sceneFile);
                continue;
            }
            EditorSceneManager.OpenScene(sceneFile, OpenSceneMode.Single);

            TeleporterObject[] teleporters = GameObject.FindObjectsOfType<TeleporterObject>();
            foreach (var teleport in teleporters)
            {
                if (!DataManager.Instance.Teleporters.ContainsKey(teleport.ID))
                {
                    EditorUtility.DisplayDialog("错误", string.Format("地图:{0} 中配置的 Teleporter:[{1}]中不存在", map.Value.Resource, teleport.ID), "确定");
                    return;
                }
                TeleporterDefine def = DataManager.Instance.Teleporters[teleport.ID];
                if (def.MapID != map.Value.ID)
                {
                    EditorUtility.DisplayDialog("错误", string.Format("地图:{0} 中配置的 Teleporter:[{1}] MapID:{2} 错误", map.Value.Resource, teleport.ID, def.MapID), "确定");
                    return;
                }
                def.Position = GameObjectTool.WorldToLogicN(teleport.transform.position);
                def.Direction = GameObjectTool.WorldToLogicN(teleport.transform.forward);
            }
        }
        DataManager.Instance.SaveTeleporters();
        EditorSceneManager.OpenScene("Assets/Levels/" + currentScene + ".unity");
        EditorUtility.DisplayDialog("提示","传送点导出完成","确认");
    }
}