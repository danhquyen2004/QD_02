using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public bool createdDust;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] public bool dead;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }
    public void CanNotFall()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void CanFall()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            EntityDie();
        }
    }
    public void TakeHealth(float health)
    {
        currentHealth += health;
    }
    public void EntityDie()
    {
        dead = true;
        rb.bodyType = RigidbodyType2D.Static;
    }

}
