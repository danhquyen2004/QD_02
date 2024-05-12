using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabby : Enemy
{
    public CrabbyVisual visual;
    public CrabbyAttack attack;
    public EnemyMovement movement;
    public ParticleSystem takeDamgeParticles;

    protected override void Start()
    {
        base.Start();
        movement = transform.Find("Movement").GetComponent<EnemyMovement>();
        visual = transform.Find("Visuals").GetComponent<CrabbyVisual>();
        attack = transform.Find("Attack").GetComponent<CrabbyAttack>();
        takeDamgeParticles = transform.GetComponentInChildren<ParticleSystem>();
    }
    public override void TakeDamage(float damage)
    {
        if (dead) return;
        base.TakeDamage(damage);
        takeDamgeParticles.Play();
        if (!dead)
        {
            visual.animator.SetTrigger("HitTrigger");
            EntityStun(TimeAnimationClip(visual.animator,"Hit"));
        }
    }
            
    
}
