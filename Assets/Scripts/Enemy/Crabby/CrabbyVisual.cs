using System.Collections;
using System.Collections.Generic;
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
        //if (crabby.movement.GroundCheck())
        //{
        //    if (crabby.rb.velocity.y <= 0)
        //        animator.SetFloat("MoveState", 3); // Fall
        //    else
        //        animator.SetFloat("MoveState", 2); // Jump
        //    return;
        //}

        animator.SetBool("LieDown",crabby.lieDown);

        if(crabby.dead)
        {
            animator.SetTrigger("DeadTrigger");
            return;
        }

        if (crabby.movement.directionMove != 0)
            animator.SetFloat("MoveState", 1); // Run
        else
            animator.SetFloat("MoveState", 0); // Idle
    }
}
