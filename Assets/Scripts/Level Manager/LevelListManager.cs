using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelListManager : MonoBehaviour
{
   
    public GameObject levelPrefab;
    public Transform levelContent;
    
    private void Start()
    {
        LoadTheLevels();
    }

    private void LoadTheLevels()
    {
        foreach (Level level in LevelManager.Instance.dataManager.levelsList.levels)
        {
            if (levelPrefab != null)
            {
                GameObject newLevel = Instantiate(levelPrefab, levelContent);
                TMP_Text levelNoText = newLevel.transform.Find("LevelNo").GetComponent<TMP_Text>();
                TMP_Text highScoreText = newLevel.transform.Find("HighScore").GetComponent<TMP_Text>();
                LevelButton levelButton = newLevel.transform.Find("PlayButton").GetComponent<LevelButton>();
                // Set the LevelNo and HighScore texts using the levelInfo data
                levelNoText.text = "Level " + level.levelNo.ToString() + " - Goal: " + level.goal.ToString();
                highScoreText.text = "High Score: " + level.highScore.ToString();
                levelButton.levelNo = int.Parse(level.levelNo);
            }

        }
        EventManager.AddListener(EventName.LevelSelected, HandleLevelSelect);
    }

    private void HandleLevelSelect(int a)
    {
     
        Destroy(gameObject);
    }

   


}
