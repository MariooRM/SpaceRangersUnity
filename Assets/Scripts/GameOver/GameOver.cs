using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI deadEnemiesText;
    public TextMeshProUGUI timesDamagedText;
    public TextMeshProUGUI totalkitsText;

    void Start() 
    {
        loadData();
    }

    void loadData()
    {
        int win = PlayerPrefs.GetInt("win");
        int score = PlayerPrefs.GetInt("score");
        int deadEnemies = PlayerPrefs.GetInt("deadEnemies");
        int timesDamaged = PlayerPrefs.GetInt("timesDamaged");
        int kits = PlayerPrefs.GetInt("totalMedkits");

        if (win == 0)
        {
            messageText.text = "Game Over!";
        }
        else
        {
            messageText.text = "Well done!";
        }

        scoreText.text = "Score: " + score.ToString();
        deadEnemiesText.text = "Dead enemies: " + deadEnemies.ToString();
        timesDamagedText.text = "Times damaged: " + timesDamaged.ToString();
        totalkitsText.text = "Medkits: " + kits.ToString();
    }


}
