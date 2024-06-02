using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkStarMovement : EnemyMovement
{
    protected PinkStar pinkStar;
    protected void Start()
    {
        Load();
        pinkStar = GetComponentInParent<PinkStar>();
    }
    void Update()
    {
        if (!pinkStar.CanMove()) return;
        if (ChangeDirectionCheck() && GroundCheck())
            directionMove *= -1;

       Move(directionMove);
    }
}
