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
    private void Start() {
        cannon = GetComponentInParent<Cannon>();
    }
    private void Update()
    {
        if (!cannon.CanMove()) return;
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
            cannon.visual.animator.SetTrigger("AttackTrigger");
        }
    }
    public bool DetectPlayer()
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
            Debug.Log("===");
            cannonBullet.SetDamge(damage);
            cannonBullet.BulletMove(firingForce,-1*transform.parent.lossyScale.x);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkPlayerPoint.transform.position, radius);
    }
}
