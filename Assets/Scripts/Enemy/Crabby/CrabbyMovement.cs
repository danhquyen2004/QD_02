using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabbyMovement : EnemyMovement
{
    protected Crabby crabby;
    protected int oldDirection;
    protected void Start()
    {
        Load();
        crabby = GetComponentInParent<Crabby>();
    }
    void Update()
    {
        if (!crabby.CanMove()) return;
        if (crabby.attack.DetectPlayer() ) return;
        if (ChangeDirectionCheck() && GroundCheck())
            directionMove *= -1;

        Move(directionMove);
    }
    
}
