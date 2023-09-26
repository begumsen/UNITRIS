using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelListManager : MonoBehaviour
{
   
    public GameObject levelPrefab;
    public Transform levelContent;
    List<LevelButton> levelButtons;

    private void Start()
    {
        levelButtons = new List<LevelButton>();
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
                Image buttonImage = levelButton.GetComponent<Image>();
                // Check if the Image component is found
                if (LevelManager.Instance.InitialLockedLevel > int.Parse(level.levelNo))
                {
                    buttonImage.color = Color.green; // Change to the desired color
                }
                else
                {
                    buttonImage.color = Color.red;
                }
                // Set the LevelNo and HighScore texts using the levelInfo data
                levelNoText.text = "Level " + level.levelNo.ToString() + " - Goal: " + level.goal.ToString();
                highScoreText.text = "High Score: " + level.highScore.ToString();
                levelButton.levelNo = int.Parse(level.levelNo);
                levelButtons.Add(levelButton);
            }

        }
    }
}
