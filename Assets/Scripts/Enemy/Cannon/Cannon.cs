using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Enemy
{
    public CannonAttack attack;
    public CannonVisual visual;
    public ParticleSystem takeDamgeParticles;
    protected override void Start() {
        base.Start();
        attack = GetComponentInChildren<CannonAttack>();
        visual = GetComponentInChildren<CannonVisual>();
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
