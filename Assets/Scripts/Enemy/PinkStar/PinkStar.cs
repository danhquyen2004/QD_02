using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkStar : Enemy
{
    public PinkStarVisual visual;
    public PinkStarAttack attack;
    public EnemyMovement movement;
    public ParticleSystem takeDamgeParticles;

    protected override void Start()
    {
        base.Start();
        movement = transform.Find("Movement").GetComponent<EnemyMovement>();
        visual = transform.Find("Visuals").GetComponent<PinkStarVisual>();
        attack = transform.Find("Attack").GetComponent<PinkStarAttack>();
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
            EntityStun(TimeAnimationClip(visual.animator, "Hit"));
        }
    }
}
