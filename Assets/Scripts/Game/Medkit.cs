using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Medkit : MonoBehaviour
{
    protected Rigidbody2D rigid;
    protected Transform trans;
    protected SpriteRenderer spriteRender;
    public float speed = 2f;

    // Start is called before the first frame update
   void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, - speed);
        trans = GetComponent<Transform>();
        spriteRender = GetComponent<SpriteRenderer>();
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
}
