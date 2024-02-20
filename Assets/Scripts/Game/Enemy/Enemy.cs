using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    protected Rigidbody2D rigid;
    protected Transform trans;
    protected SpriteRenderer spriteRender;
    protected EnemyModel enemyModel;
    public GameObject prefabShot;
    protected float initialTime;
    private HUD hud;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, - speed);
        trans = GetComponent<Transform>();
        spriteRender = GetComponent<SpriteRenderer>();
        enemyModel = new EnemyModel();

         // We call the function shoot in a random range between 2 and 5 secs
        initialTime = Random.Range(2f, 5f);
        InvokeRepeating("shoot", initialTime, UnityEngine.Random.Range(1f, 3f));
        hud = FindObjectOfType<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        //The bottom-left of the viewport is (0,0); the topright is (1,1).
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //Tengo en cuenta el borde inferior del sprite
        if (vector2min.y > spriteRender.bounds.max.y) Destroy(this.gameObject);
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.name.StartsWith("Bullet"))
        {
            enemyModel.pHp -= 100;
            Destroy(other.gameObject);
        }

        if (enemyModel.pHp <= 0 || other.gameObject.name.StartsWith("Player"))
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;
            Animator animator = GetComponent<Animator>();
            animator.SetBool("explode", true);
        }
    }

    void destroyInstance(){
        Destroy(gameObject);
        Player player;
        player = GameObject.Find("Player").GetComponent<Player>();
        player.getPlayer().pScore += 5;
        player.getPlayer().pDeadEnemies += 1;
        hud.updateScore(player.getPlayer().pScore);
    }

    void shoot(){
        Instantiate(prefabShot, new Vector2(trans.GetChild(0).position.x, trans.GetChild(0).position.y), Quaternion.identity);
    }

    
}
