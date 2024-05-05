using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabby : EntityManager
{
    public CrabbyVisual visual;
    public EnemyMovement movement;

    protected override void Start()
    {
        base.Start();
        movement = transform.Find("Movement").GetComponent<EnemyMovement>();
        visual = transform.Find("Visuals").GetComponent<CrabbyVisual>();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (!dead)
            visual.animator.SetTrigger("HitTrigger");
        Debug.Log("dasd");
    }
}
