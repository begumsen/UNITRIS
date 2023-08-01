using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The HUD for the game
/// </summary>
public class HUD : EventInvoker
{


    // score support
    [SerializeField]
    TextMeshProUGUI scoreText;
    int score = 0;

    // health support
    float maxHealth = 4f;
    float health;
    [SerializeField]
    Slider healthBar;
    public Image fill;
    public Gradient gradient;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        fill.color = gradient.Evaluate(1f);
        
        // add listener for PointsAddedEvent
        EventManager.AddListener(EventName.PointsAdded, HandlePointsAddedEvent);
        // initialize score text
        scoreText.text = "Score: " + score;
        // add listener for HealthChangedEvent
        EventManager.AddListener(EventName.Damage, HandleHealthChangedEvent);
        events.Add(EventName.GameOver, new GameOverEvent());
        EventManager.AddInvoker(EventName.GameOver, this);
       
    }


    /// <summary>
    /// Gets the score
    /// </summary>
    /// <value>the score</value>
    public int Score
    {
        get { return score; }
    }


    #region Private methods

    /// <summary>
    /// Handles the points added event by updating the displayed score
    /// </summary>
    /// <param name="points">points to add</param>
    private void HandlePointsAddedEvent(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    /// <summary>
    /// Handles the health changed event by changing
    /// the health bar value
    /// </summary>
    /// <param name="value">health value</param>
    void HandleHealthChangedEvent(int value)
    {
        health -= value;
        healthBar.value = health;
        fill.color = gradient.Evaluate(health / maxHealth);

        if(health == 0)
        {
            events[EventName.GameOver].Invoke(0);
        }
    }

  
    #endregion
}
