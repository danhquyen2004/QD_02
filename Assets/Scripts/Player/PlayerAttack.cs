using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] protected float coolDownAtk = 1;
    protected float timer = 1;
    protected bool attacked;
    public bool attacking;  // attacking == true => Can't move;

    [Header("Attack Zone")]
    [SerializeField] protected GameObject attackPoint;
    [SerializeField] protected float radius;

    [Header("Air Attack Zone")]
    [SerializeField] protected GameObject airAttackPoint;
    [SerializeField] protected float airRadius;



    private void Start()
    {
        timer = coolDownAtk;
    }
    void Update()
    {
        //Debug.DrawRay(PlayerManager.Instance.transform.position, Vector3.down*2, Color.yellow);
        if (CanAttack())
        {
            RaycastHit2D ray = Physics2D.Raycast(PlayerManager.Instance.transform.position, Vector3.down, 2, ~LayerMask.GetMask("Player"));
            if(ray.collider != null)
            {
                Attack();
            }
            else
            {
                AirAttack();
            }
        }
            
    }
    private void Attack()
    {
        if (InputManager.Instance.Attack)
        {
            attacked = true;
            attacking = true;
            PlayerManager.Instance.visual.animator.SetTrigger("AttackTrigger");
        }
    }
    private void AirAttack()
    {
        if (InputManager.Instance.Attack)
        {
            attacked = true;
            PlayerManager.Instance.CanNotFall();
            PlayerManager.Instance.visual.animator.SetFloat("AttackState", 3);
            PlayerManager.Instance.visual.animator.SetTrigger("AttackTrigger");
        }
    }

    public bool CanAttack()
    {
        if (attacked)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                return false;
            }
            else
            {
                attacked = false;
                timer = coolDownAtk;
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    public bool Attack1()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //Do some thing
                return true;
            }
        }
        return false;
    }
    public bool Attack2()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                // do some thing
                return true;
            }

        }
        return false;
    }
    public bool Attack3()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                // do some thing
                return true;
            }
        }
        return false;
    }

    public void AirAttack1()
    {
        PlayerManager.Instance.rb.velocity = Vector2.zero;
        Collider2D[] cols = Physics2D.OverlapCircleAll(airAttackPoint.transform.position, airRadius);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //Do some thing
            }
        }
    }
    public void AirAttack2()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(airAttackPoint.transform.position, airRadius);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //Do some thing
            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    //    Gizmos.DrawWireSphere(airAttackPoint.transform.position, airRadius);
    //}
}
