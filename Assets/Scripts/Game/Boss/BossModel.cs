using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModel 
{
    protected int hp = 4500;
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
