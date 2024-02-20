using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class EnemyShot : MonoBehaviour
{

    public float speed = 5.0f;
    protected Rigidbody2D rigidBody;
    protected SpriteRenderer render;
    protected Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, -speed);

        trans = GetComponent<Transform>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (vector2min.y > render.bounds.max.y) Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.StartsWith("Player"))
        {
            Destroy(gameObject);
        }
    }
}
