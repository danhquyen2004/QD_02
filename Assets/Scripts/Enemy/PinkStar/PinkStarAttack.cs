using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PinkStarAttack : EnemyAttack
{
    private PinkStar pinkStar;

    [Header("Attack Zone")]
    [SerializeField] protected Transform attackPoint;

    [SerializeField] protected float radiusHit;
    [SerializeField] protected float radiusDetect;
    [HideInInspector] public bool attacking;  // attacking == true => Run attack move;

    private float oldSpeed;
    private void Start()
    {
        pinkStar = GetComponentInParent<PinkStar>();
        timer = coolDown;
        attackPoint = transform.parent;
    }
    private void Update()
    {
        if (!pinkStar.CanMove()) return;
        if (CanAttack())
        {
            Attack();
        }

        if(oldSpeed!=0 && !attacking)
        {
            pinkStar.movement.speed = oldSpeed;
        }
    }
    private void Attack()
    {
        if (DetectPlayer())
        {
            attacked = true;
            attacking = true;
            AttackMovement();
            pinkStar.visual.animator.SetTrigger("AttackTrigger");
        }
    }
    public void AttackMovement()
    {
        oldSpeed = pinkStar.movement.speed;
        pinkStar.movement.speed = pinkStar.movement.speed * 2;
    }
    public void AttackEven()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPoint.position, radiusHit);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerManager player = col.gameObject.GetComponent<PlayerManager>();

                //Do some thing
                player.TakeDamage(damage);
            }
        }

    }
    public bool DetectPlayer()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPoint.position, radiusDetect);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return true;
            }
        }
        return false;
    }
    public bool HitPlayer()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPoint.position, radiusHit);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, radiusHit);
        Gizmos.DrawWireSphere(attackPoint.position, radiusDetect);

        //Gizmos.DrawWireSphere(airAttackPoint.transform.position, airRadius);
    }
}
