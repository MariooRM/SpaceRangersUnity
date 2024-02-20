using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    protected int hp = 100;
    protected int score = 0;
    protected float speed;
    protected int deadEnemies = 0;
    protected int timesDamaged = 0;
    protected int totalKits = 0;

    public int pHp
    {
        get { return hp; }
        set { hp = value; }
    }

    public int pScore
    {
        get { return score; }
        set { score = value; }
    }

    public float pSpeed
    {
        get { return speed; }
        set { speed = value; }
    }

    public int pDeadEnemies
    {
        get { return deadEnemies; }
        set { deadEnemies = value; }
    }

    public int pTotalKits
    {
        get { return totalKits; }
        set { totalKits = value; }
    }

    public int pTimesDamaged
    {
        get { return timesDamaged; }
        set { timesDamaged = value; }
    }


}
