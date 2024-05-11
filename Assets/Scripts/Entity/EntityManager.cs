using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public bool createdDust;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] public bool dead;
    [HideInInspector] public bool stun;

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
        gameObject.GetComponent<Collider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }
    public void EntityStun(float time)
    {
        stun = true;
        Invoke(nameof(OutOfStun), time);
    }
    public void OutOfStun()
    {
        stun = false;
    }
    public float TimeAnimationClip(Animator animator,string nameClip)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
            if (clip.name == nameClip)
                return clip.length;
        return 0;
    }
}
