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
    [SerializeField] protected float distanceDetect;
    [HideInInspector] public bool attacking;  // attacking == true => Run attack move;

    private Vector2 rollPoint;
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

        if (attacking)
        {
            HitPlayer();
            transform.parent.Translate(rollPoint * Time.deltaTime * 2f);

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
        Debug.Log("Attack Event");
        attacking = true;
    }
    public void EndAttackEvent()
    {
        attacking = false;
        rollPoint = Vector2.zero;
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
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPoint.position, radiusHit);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerManager player = col.gameObject.GetComponent<PlayerManager>();

                //Do some thing
                Debug.Log("Hit Player");
                player.TakeDamage(damage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, radiusHit);
        Gizmos.DrawWireSphere(attackPoint.position, distanceDetect);

        //Gizmos.DrawWireSphere(airAttackPoint.transform.position, airRadius);
    }
}
