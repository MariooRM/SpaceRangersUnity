using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Animator), typeof(AudioSource))]

public class Player : MonoBehaviour {
    public float speed = 5.0f;
    public float delay = 0.05f;
    private float lastShot;
    protected Transform trans;
    protected Animator anim;
    public GameObject prefabShot;
    protected AudioSource audioS;
    protected PlayerModel playerModel;
    public HUD hud;


     // Use this for initialization
     void Start ()
     {
        trans = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        lastShot = 0f;
        audioS = GetComponent<AudioSource>();
        audioS.pitch = 2f;

        playerModel = new PlayerModel();
        
        playerModel.pSpeed = speed;
        playerModel.pScore = 0;
    }
 
     // Update is called once per frame
     void Update ()
     {
        Vector2 vector2 = new Vector2();
        vector2.x = Input.GetAxisRaw("Horizontal");
        vector2.y = Input.GetAxisRaw("Vertical");
        vector2.Normalize(); //el módulo sea 1
        move(vector2);

        if (Time.time - lastShot >= delay){

            if (Input.GetAxisRaw("Fire1") == 1)
            {
                anim.SetBool("shoot", true);
                shoot();
                lastShot = Time.time;
            }
            else
            {
                anim.SetBool("shoot", false);
                audioS.loop = false;
            }
        }

     }

     void move(Vector2 vector2)
     {
     //calculamos las coordenadas mínima y máxima en coordenadas del mundo

        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        vector2.x *= speed * Time.deltaTime;
        vector2.y *= speed * Time.deltaTime;

        vector2.x = Mathf.Clamp(trans.position.x + vector2.x, vector2min.x, vector2max.x);
        vector2.y = Mathf.Clamp(trans.position.y + vector2.y, vector2min.y, vector2max.y);
    
        trans.position = new Vector2(vector2.x, vector2.y);
     }

     void shoot()
     {
        Instantiate(prefabShot, new Vector2(trans.GetChild(0).position.x, trans.GetChild(0).position.y), Quaternion.identity);
        Instantiate(prefabShot, new Vector2(trans.GetChild(1).position.x, trans.GetChild(1).position.y), Quaternion.identity);

        if (audioS.loop == false)
        {
            audioS.loop = true;
            audioS.Play();
        }

      }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Enemy") || collider.gameObject.tag.Equals("EnemyShot") || collider.gameObject.tag.Equals("Boss"))
        {
            playerModel.pHp -= 40;
            playerModel.pTimesDamaged += 1;
            
        }

        if (collider.gameObject.tag.Equals("Medkit"))
        {
            playerModel.pTotalKits += 1;
            Destroy(collider.gameObject);
            
            if (playerModel.pHp < 100)
            {
                playerModel.pHp += 20;
            }
        } 

        hud.updateLives(playerModel.pHp);

        if (playerModel.pHp <= 0)
        {
            Destroy(gameObject);

            /* Information for the Game Over scene*/
            PlayerPrefs.SetInt("win", 0);
            PlayerPrefs.SetInt("score", playerModel.pScore);
            PlayerPrefs.SetInt("timesDamaged", playerModel.pTimesDamaged);
            PlayerPrefs.SetInt("totalMedkits", playerModel.pTotalKits);
            PlayerPrefs.SetInt("deadEnemies", playerModel.pDeadEnemies);

            /* Loading Game Over scene*/
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } 

    }

    public PlayerModel getPlayer()
    {
        return playerModel;
    }
}
