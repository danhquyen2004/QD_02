using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeashellAttack : CannonAttack
{
    public override bool DetectPlayer()
    {

        RaycastHit2D[] rays = Physics2D.RaycastAll(checkPlayerPoint.transform.position,Vector2.left*transform.parent.localScale.x,radius);
        foreach (var ray in rays)
        {
            if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return true;
            }
        }
        return false;
    }
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(checkPlayerPoint.transform.position,Vector2.left*transform.parent.localScale.x*radius);
    }
}
