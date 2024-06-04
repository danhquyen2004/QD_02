using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    protected float damage;
    [SerializeField] protected Rigidbody2D rb;
    protected virtual void OnEnable() {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetDamge(float damage)
    {
        this.damage = damage;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            other.GetComponent<PlayerManager>().TakeDamage(damage);
        }
    }
}
