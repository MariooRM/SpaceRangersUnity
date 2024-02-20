using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel 
{
    protected int hp = 100;
    protected float speed;

    public int pHp
    {
        get {return hp;}
        set {hp = value;}
    }

    public float pSpeed
    {
        get {return speed;}
        set {speed = value;}
    }
}
