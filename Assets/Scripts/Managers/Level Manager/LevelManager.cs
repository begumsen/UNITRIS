using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    LevelList levelList = new LevelList();
    TextAsset levelJson;
    string fileName = "levelJson";
    public GameObject levelPrefab;
    public Transform levelContent;
    // Start is called before the first frame update
    void Awake()
    {
        
        levelJson = Resources.Load<TextAsset>(fileName);
        levelList = JsonUtility.FromJson<LevelList>(levelJson.text);
    }

    private void Start()
    {
        foreach (Level level in levelList.levels)
        {
            if(levelPrefab != null)
            {
                GameObject newLevel = Instantiate(levelPrefab, levelContent);
                TMP_Text levelNoText = newLevel.transform.Find("LevelNo").GetComponent<TMP_Text>();
                TMP_Text highScoreText = newLevel.transform.Find("HighScore").GetComponent<TMP_Text>();

                // Set the LevelNo and HighScore texts using the levelInfo data
                levelNoText.text = "Level " + level.levelNo.ToString();
                highScoreText.text = "High Score: " + level.highScore.ToString();
            }
            
        }
    }

    public LevelList LevelList
    {
        get
        {
            return LevelList;
        }
    }


}
