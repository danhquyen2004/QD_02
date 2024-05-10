using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 3;

    [SerializeField] protected float coolDownAtk = 1;
    protected float timer = 1;
    [HideInInspector] public bool attacked;
    [HideInInspector] public bool airAttacked;
    public bool attacking;  // attacking == true => Can't move;


    [Header("Attack Zone")]
    [SerializeField] protected GameObject attackPoint;
    [SerializeField] protected float radius;

    [Header("Air Attack Zone")]
    [SerializeField] protected GameObject airAttackPoint;
    [SerializeField] protected float airRadius;

    private int playerAndEnemyLayerMask;

    private void Start()
    {
        playerAndEnemyLayerMask = LayerMask.GetMask("Player", "Enemy");
        timer = coolDownAtk;
    }
    void Update()
    {
        if (PlayerManager.Instance.movement.GroundCheck()) airAttacked = false;

        Debug.DrawRay(PlayerManager.Instance.transform.position, Vector3.down*2, Color.yellow);
        if (CanAttack())
        {
            RaycastHit2D ray = Physics2D.Raycast(PlayerManager.Instance.transform.position, Vector3.down, 2, ~playerAndEnemyLayerMask);
            if (ray.collider != null && PlayerManager.Instance.movement.GroundCheck())
            {
                Attack();
                return;
            }
            if (ray.collider == null && CanAirAttack())
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
            airAttacked = true;
            PlayerManager.Instance.CanNotFall();
            PlayerManager.Instance.visual.animator.Play("AirAttack");
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
    public bool CanAirAttack()
    {
        return !airAttacked;
    }
    public bool Attack1()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //Do some thing
                col.gameObject.GetComponent<EntityManager>().TakeDamage(damage);
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
                col.gameObject.GetComponent<EntityManager>().TakeDamage(damage+1);
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
                col.gameObject.GetComponent<EntityManager>().TakeDamage(damage+2);
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(ForceDirection(400));
                return true;
            }
        }
        return false;
    }
    private Vector3 ForceDirection(float force)
    {
        Vector3 forceDirection;
        if (PlayerManager.Instance.transform.localScale.x > 0)
        {
            forceDirection = Quaternion.Euler(0, 0, 80) * Vector3.right * force;
        }
        else
        {
            forceDirection = Quaternion.Euler(0, 0, 100) * Vector3.right * force;
        }
        return forceDirection;
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
