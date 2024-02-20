using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Boss : MonoBehaviour
{
    public float speed = 1f;
    protected Rigidbody2D rigid;
    protected Transform trans;
    protected SpriteRenderer spriteRender;
    public GameObject prefabShot;
    protected BossModel bossModel;
    protected Vector3 startPosition;
    protected Vector3 endPosition;
    protected float lerpTime = 2f;
    protected bool firstTime = true;
    protected float initialTime;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, -speed);
        trans = GetComponent<Transform>();
        spriteRender = GetComponent<SpriteRenderer>();

        bossModel = new BossModel();

        // Inicia la corrutina para mover el boss suavemente
        StartCoroutine(MoveBossSmoothly());
        InvokeRepeating("shoot", initialTime, UnityEngine.Random.Range(1f, 3f));
    }

    IEnumerator MoveBossSmoothly()
    {
        // The bottom-left of the viewport is (0,0); the top-right is (1,1).
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        while (true)
        {
            if (trans.position.y <= 1.6)
            {
                if (firstTime)
                {
                    rigid.velocity = Vector2.zero;
                    startPosition = trans.position;
                    endPosition = generateRandomEndPosX(vector2min, vector2max);
                    firstTime = false;
                }

                // Mueve el boss hacia la posición final con velocidad constante
                trans.position = Vector3.MoveTowards(trans.position, endPosition, speed * Time.deltaTime);

                if (Vector3.Distance(trans.position, endPosition) < 0.01f)
                {
                    startPosition = trans.position;
                    endPosition = generateRandomEndPosX(vector2min, vector2max);
                }
            }

            yield return null; // Espera hasta el siguiente fotograma
        }
    }

    Vector3 generateRandomEndPosX(Vector2 vector2min, Vector2 vector2max)
    {
        float randomX = UnityEngine.Random.Range(vector2min.x + 1, vector2max.x - 1);
        Vector3 randomPos = new Vector3(randomX, trans.position.y, trans.position.z);

        return randomPos;
    }

    void destroyInstance()
    {
        Destroy(gameObject);
        Player player;
        player = GameObject.Find("Player").GetComponent<Player>();
        player.getPlayer().pScore += 1000;

        /* Information for the Game Over scene*/
        PlayerPrefs.SetInt("win", 1);
        PlayerPrefs.SetInt("score", player.getPlayer().pScore);
        PlayerPrefs.SetInt("timesDamaged", player.getPlayer().pTimesDamaged);
        PlayerPrefs.SetInt("totalMedkits", player.getPlayer().pTotalKits);
        PlayerPrefs.SetInt("deadEnemies", player.getPlayer().pDeadEnemies);

        /* Loading Game Over scene*/
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void shoot()
    {
        Instantiate(prefabShot, new Vector2(trans.GetChild(0).position.x, trans.GetChild(0).position.y), Quaternion.identity);
        Instantiate(prefabShot, new Vector2(trans.GetChild(1).position.x, trans.GetChild(1).position.y), Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.StartsWith("Bullet") || collider.gameObject.name.StartsWith("Player"))
        {
            bossModel.pHp -= 10;
        }

        if (bossModel.pHp <= 0)
        {
            Collider2D other = GetComponent<Collider2D>();
            other.enabled = false;
            Animator anim = GetComponent<Animator>();
            anim.SetBool("explode", true);
        }
    }

    public BossModel getBossModel()
    {
        return bossModel;
    }
}
