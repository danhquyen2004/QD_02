using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 3;
    [SerializeField] protected float coolDown = 1;
    protected float timer = 1;
    [HideInInspector] public bool attacked;
    [HideInInspector] public bool airAttacked;


    [Header("Attack Zone")]
    [SerializeField] protected GameObject attackPoint;
    [SerializeField] protected float radius;
    [HideInInspector] public bool attacking;  // attacking == true => Can't move;

    [Header("Air Attack Zone")]
    [SerializeField] protected GameObject airAttackPoint;
    [SerializeField] protected float airRadius;

    [Header("Throw Sword Zone")]
    [SerializeField] protected GameObject swordPrefab;
     public bool holdingSword;
    [SerializeField] private GameObject newSword;

    private int playerAndEnemyLayerMask;

    private void Start()
    {
        holdingSword = true;
        playerAndEnemyLayerMask = LayerMask.GetMask("Player", "Enemy");
        timer = coolDown;
    }
    void Update()
    {
        if (PlayerManager.Instance.movement.GroundCheck()) airAttacked = false;
        ResetSword();

        ThrowSword();
        if (!holdingSword) return;
        //Debug.DrawRay(PlayerManager.Instance.transform.position, Vector3.down*2, Color.yellow);
        if (CanAttack())
        {
            RaycastHit2D ray = Physics2D.Raycast(PlayerManager.Instance.transform.position, Vector3.down, 2, ~playerAndEnemyLayerMask);
            if (ray.collider != null)
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

    private bool CanAttack()
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
                timer = coolDown;
                return true;
            }
        }
        else
        {
            return true;
        }
    }
    private bool CanAirAttack()
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
                col.gameObject.GetComponent<EntityManager>().TakeDamage(damage + 1);
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
                Enemy enemy = col.gameObject.GetComponent<Enemy>();

                // enemy take damge
                enemy.TakeDamage(damage + 2);

                // enemy lie down
                if (enemy.rb.bodyType == RigidbodyType2D.Dynamic)
                    enemy.rb.velocity = Vector3.zero;
                enemy.rb.AddForce(ForceDirection(650));
                enemy.StartLie(1.5f);

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
            forceDirection = Quaternion.Euler(0, 0, 45) * Vector3.right * force;
        }
        else
        {
            forceDirection = Quaternion.Euler(0, 0, 135) * Vector3.right * force;
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
                col.gameObject.GetComponent<EntityManager>().TakeDamage(damage + 1);
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
                Enemy enemy = col.gameObject.GetComponent<Enemy>();

                // enemy take damge
                enemy.TakeDamage(damage + 2);

                // enemy lie down
                if (enemy.rb.bodyType == RigidbodyType2D.Dynamic)
                    enemy.rb.velocity = Vector3.zero;
                enemy.rb.AddForce(ForceDirection(650));
                enemy.StartLie(1.5f);
            }
        }
    }

    public void ResetSword()
    {
        holdingSword = newSword == null;
    }
    public void ThrowSword()
    {
        if (InputManager.Instance.Attack2)
        {
            if (holdingSword)
            {
                attacked = true;
                holdingSword = false;
                PlayerManager.Instance.visual.animator.SetTrigger("AttackTrigger");
                PlayerManager.Instance.visual.animator.SetFloat("AttackState", 3);
                Invoke(nameof(ChangeWithoutSword),
                    PlayerManager.Instance.TimeAnimationClip(PlayerManager.Instance.visual.animator, "ThrowSword"));
            }
            else
            {
                if (newSword.GetComponent<Sword>().canPick)
                    PlayerManager.Instance.transform.position = newSword.transform.position;
            }
        }
    }
    private void ChangeWithoutSword()
    {
        PlayerManager.Instance.visual.SetLayerWithoutSword(1);
    }
    public void InstantiateSword()
    {
        newSword = Instantiate(swordPrefab, attackPoint.transform.position, Quaternion.identity);
        newSword.GetComponent<Sword>().SetDir(PlayerManager.Instance.transform.localScale.x);
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    //    //Gizmos.DrawWireSphere(airAttackPoint.transform.position, airRadius);
    //}

}
