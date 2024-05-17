using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabbyAttack : EnemyAttack
{
    private Crabby crabby;

    [Header("Attack Zone")]
    [SerializeField] protected GameObject[] attackPoints;

    [SerializeField] protected float radius;
    [HideInInspector] public bool attacking;  // attacking == true => Can't move;
    private void Start()
    {
        crabby = GetComponentInParent<Crabby>();
        timer = coolDown;
    }
    private void Update()
    {
        if (!crabby.CanMove()) return;
        if (CanAttack())
        {
            Attack();
        }
    }
    private void Attack()
    {
        if (DetectPlayer())
        {
            attacked = true;
            attacking = true;
            crabby.visual.animator.SetTrigger("AttackTrigger");
        }
    }
    
    public void AttackEven()
    {
        foreach (GameObject point in attackPoints)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(point.transform.position, radius);
            foreach(Collider2D col in cols)
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    PlayerManager player = col.gameObject.GetComponent<PlayerManager>();

                    //Do some thing
                    player.TakeDamage(damage);
                }
            }
        }
    }
    public bool DetectPlayer()
    {
        foreach (GameObject point in attackPoints)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(point.transform.position, radius);
            foreach (Collider2D col in cols)
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (GameObject point in attackPoints)
        {
            Gizmos.DrawWireSphere(point.transform.position, radius);
        }

        //Gizmos.DrawWireSphere(airAttackPoint.transform.position, airRadius);
    }
}
