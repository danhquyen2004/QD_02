using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAttack : EnemyAttack
{
    Cannon cannon;
    public float firingForce;
    [SerializeField] protected GameObject attackPoint;
    [SerializeField] protected GameObject checkPlayerPoint;
    [SerializeField] protected float radius;
    [SerializeField] protected GameObject cannonBall;
    protected void Start() {
        cannon = GetComponentInParent<Cannon>();
    }
    protected void Update()
    {
        if (!cannon.CanMove()) return;
        if (CanAttack())
        {
            Attack();
        }
    }
    protected void Attack()
    {
        if (DetectPlayer())
        {
            attacked = true;
            cannon.visual.animator.SetTrigger("AttackTrigger");
        }
    }
    public virtual bool DetectPlayer()
    {

        Collider2D[] cols = Physics2D.OverlapCircleAll(checkPlayerPoint.transform.position, radius);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return true;
            }
        }
        return false;
    }
    public void AttackEven()
    {
        GameObject bullet = Instantiate(cannonBall,attackPoint.transform.position,Quaternion.identity);
        bullet.SetActive(true);
        CannonBullet cannonBullet = bullet.GetComponent<CannonBullet>();
        if (cannonBullet != null)
        {
            cannonBullet.SetDamge(damage);
            cannonBullet.BulletMove(firingForce,-1*transform.parent.lossyScale.x);
        }
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkPlayerPoint.transform.position, radius);
    }
}
