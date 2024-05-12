using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CrabbyVisual : MonoBehaviour
{
    public Crabby crabby;
    public Animator animator;
    private void Start()
    {
        crabby = transform.parent.GetComponent<Crabby>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        AnimationHandling();
    }
    private void AnimationHandling()
    {
        animator.SetBool("LieDown",crabby.lieDown);

        if(crabby.dead)
        {
            animator.SetTrigger("DeadTrigger");
            return;
        }

        if (crabby.movement.directionMove != 0)
        {
            if (crabby.rb.velocity.x != 0)
                animator.SetFloat("MoveState", 1); // Run
            else
                animator.SetFloat("MoveState", 0); // Idle
        }
    }

    public void AttackEvent()
    {
        crabby.attack.attacking = false;
        crabby.attack.AttackEven();
    }
}
