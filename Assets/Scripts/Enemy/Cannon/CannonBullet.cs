using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : EnemyBullet
{
    Animator animator;
    protected override void OnEnable() {
        base.OnEnable();
        animator = GetComponent<Animator>();
    }
    public void BulletMove(float force,float direction)
    {
        rb.velocity = new Vector2(direction*force,rb.velocity.y);
    }
    protected override void OnTriggerEnter2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Enemy") return;
        base.OnTriggerEnter2D(other);
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("ExplosionTrigger");
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
