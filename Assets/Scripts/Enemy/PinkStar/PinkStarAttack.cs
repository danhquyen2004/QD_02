using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PinkStarAttack : EnemyAttack
{
    private PinkStar pinkStar;

    [Header("Attack Zone")]
    [SerializeField] protected Transform attackPoint;

    [SerializeField] protected float radiusHit;
    [SerializeField] protected float distanceDetect;
    [HideInInspector] public bool attacking;  // attacking == true => Run attack move;
    [HideInInspector] public bool canTakeDmg;
    private Vector2 rollPoint;
    private void Start()
    {
        pinkStar = GetComponentInParent<PinkStar>();
        timer = coolDown;
        attackPoint = transform.parent;
        canTakeDmg = true;
    }
    private void Update()
    {
        if (!pinkStar.CanMove()) return;
        if (CanAttack())
        {
            Attack();
        }

        if (attacking)
        {
            HitPlayer();
            transform.parent.Translate(rollPoint * Time.deltaTime * 2f);
            if (pinkStar.movement.WallCheck())
            {
                EndAttackEvent();
            }
        }
    }
    private void Attack()
    {
        if (DetectPlayer())
        {
            attacked = true;
            pinkStar.visual.animator.SetTrigger("AttackTrigger");
        }
    }
    public void AttackEven()
    {

        if (attackPoint.position.x < PlayerManager.Instance.transform.position.x)
            rollPoint = new Vector2(attackPoint.position.x + distanceDetect * 2, attackPoint.position.y);
        else
            rollPoint = new Vector2(attackPoint.position.x - distanceDetect * 2, attackPoint.position.y);
        attacking = true;
    }
    public void EndAttackEvent()
    {
        canTakeDmg = true;
        attacking = false;
        rollPoint = Vector2.zero;
        pinkStar.movement.directionMove *= -1;
    }
    public bool DetectPlayer()
    {
        RaycastHit2D[] rays = Physics2D.RaycastAll(attackPoint.position, Vector2.right * pinkStar.movement.directionMove, distanceDetect);
        Debug.DrawRay(attackPoint.position, Vector2.right * pinkStar.movement.directionMove * distanceDetect);
        foreach (var ray in rays)
        {
            if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return true;
            }
        }
        return false;
    }


    public void HitPlayer()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.parent.position, radiusHit);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerManager player = col.gameObject.GetComponent<PlayerManager>();

                //Do some thing
                if (canTakeDmg)
                {
                    player.TakeDamage(damage);
                    canTakeDmg = false;
                }

            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.parent.position, radiusHit);

        //Gizmos.DrawWireSphere(airAttackPoint.transform.position, airRadius);
    }
}
