using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject gOEnemy;
    private IEnumerator cor;
    public GameObject gOMedkit;
    public GameObject gOBoss;
    private float delay;
    private float lastKit;
    private bool bossIn;
    private Player player;
    private Boss boss;
    private bool newGeneration;

    // Start is called before the first frame update
    void Start()
    {
        cor = coroutinaPlanes(2);
        StartCoroutine(cor);
        lastKit = 0f;
        delay = UnityEngine.Random.Range(10f, 20f);
        bossIn = false;
        newGeneration = true;
        player = GameObject.Find("Player").GetComponent<Player>();

    }

    void Update()
    {
        if (Time.time - delay >= lastKit)
        {
            generateMedkit();
            lastKit = Time.time;
        }

        if (player.getPlayer().pScore >= 350 && !bossIn)
        {
            StopCoroutine(cor);
            generateBoss();
            bossIn = true;
        }

        if (bossIn)
        {
            if (boss != null && boss.getBossModel().pHp <= 1000 && newGeneration)
            {
                StartCoroutine(coroutinaPlanes(1));
                newGeneration = false;
            }
        }
        

    }

    private IEnumerator coroutinaPlanes(float waitTime)
    {
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        int maxPlanesNumber = 1; // Ajusta el n�mero m�ximo de aviones por generaci�n

        while (true)
        {
            int planes = (int)(UnityEngine.Random.value * maxPlanesNumber) + 1;
            maxPlanesNumber += 2; // Aumenta el m�ximo de aviones para la pr�xima generaci�n

            for (int i = 0; i < planes; i++)
            {
                GameObject gameObjectPlane = Instantiate(gOEnemy);
                float xrange = vector2max.x - vector2min.x;
                float yrange = (vector2max.y - vector2min.y) ;
                Vector2 vector2 = new Vector2(vector2min.x + UnityEngine.Random.Range(0f, 1f) * xrange, vector2max.y + UnityEngine.Random.Range(0f, 1f) * yrange);
                gameObjectPlane.transform.position = vector2;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void generateMedkit()
    {
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject gameObjectMedkit = Instantiate(gOMedkit);
        float xrange = vector2max.x - vector2min.x;
        float yrange = (vector2max.y - vector2min.y);
        Vector2 vector2 = new Vector2(vector2min.x + UnityEngine.Random.Range(0f, 1f) * xrange, vector2max.y + UnityEngine.Random.Range(0f, 1f) * yrange);
        gameObjectMedkit.transform.position = vector2;
        
    }

    private void generateBoss()
    {
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject gameObjectBoss = Instantiate(gOBoss);
        Vector2 spawnPos = new Vector2(0f, 5.35f);
        gameObjectBoss.transform.position = spawnPos;
        boss = gameObjectBoss.GetComponent<Boss>();
        

    }
}
