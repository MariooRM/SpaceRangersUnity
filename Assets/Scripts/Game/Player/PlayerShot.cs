using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerShot : MonoBehaviour
{
    public float speed = 15.0f;
    protected Rigidbody2D rigidBody;
    protected SpriteRenderer render;
    protected Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, speed);

        trans = GetComponent<Transform>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (vector2max.y < render.bounds.min.y) Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.StartsWith("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
