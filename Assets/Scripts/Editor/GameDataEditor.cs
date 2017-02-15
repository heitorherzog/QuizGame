using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class GameDataEditor : EditorWindow
{

    private string gameDataProjectFIlePath = "/StreamingAssets/data.json";
    public GameData gameData;

    [MenuItem("Window/Game Data Editor")]
    static void Init()
    {
        GameDataEditor window = (GameDataEditor)EditorWindow.GetWindow(typeof(GameDataEditor));
        window.Show();
    }

    void OnGUI( )
    {
        if(gameData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("gameData");

            EditorGUILayout.PropertyField(serializedProperty,true);
            serializedObject.ApplyModifiedProperties();

            if(GUILayout.Button("Save data"))
            {
                SaveGameData();
            }
        }

        if(GUILayout.Button("Load data"))
        {
            LoadGameData();
        }


    }
    


 

    private void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataProjectFIlePath;

        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
            gameData = new GameData();
    }

    private void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + gameDataProjectFIlePath;
        File.WriteAllText(filePath,dataAsJson);
    }

	
}
