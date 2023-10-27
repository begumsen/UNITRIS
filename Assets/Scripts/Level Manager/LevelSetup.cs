using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
 
    private const string FIRST_PLAY_KEY = "FirstPlay";
    private const string JSON_FILE_NAME = "levelJson";

    private void Awake()
    {
        bool isFirstPlay = PlayerPrefs.GetInt(FIRST_PLAY_KEY, 0) == 0;

        if (isFirstPlay)
        {
            LoadAndSaveJSON();
            PlayerPrefs.SetInt(FIRST_PLAY_KEY, 1);
        }
    }

    private void LoadAndSaveJSON()
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>("Levels/"+JSON_FILE_NAME);
        if (jsonAsset != null)
        {
            string json = jsonAsset.text;
            string filePath = Path.Combine(Application.persistentDataPath, "levelJson.txt");
            File.WriteAllText(filePath, json);


        }
        else
        {
            Debug.LogWarning("Highscores JSON not found in Resources folder.");
        }
    }

}
