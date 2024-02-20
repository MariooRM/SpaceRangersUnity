using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public GameObject[] lives;
    public TextMeshProUGUI puntos;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateLives(int hp){

        int lives_active = (hp / 20);
        

        for (int i = 0; i < lives_active; i++)
        {
            lives[i].SetActive(true);
        }

        for (int i = lives_active; i < 5; i++)
        {
            lives[i].SetActive(false);
        }
        
    }

    public void updateScore(int points){
        puntos.text = points.ToString();
    }
}
