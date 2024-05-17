using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkStarVisual : MonoBehaviour
{
    public PinkStar pinkStar;
    public Animator animator;
    private void Start()
    {
        pinkStar = transform.parent.GetComponent<PinkStar>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        AnimationHandling();
    }
    private void AnimationHandling()
    {
        animator.SetBool("LieDown", pinkStar.lieDown);

        if (pinkStar.dead)
        {
            animator.SetTrigger("DeadTrigger");
            return;
        }

        if (pinkStar.movement.directionMove != 0)
        {
            if (pinkStar.rb.velocity.x != 0)
                animator.SetFloat("MoveState", 1); // Run
            else
                animator.SetFloat("MoveState", 0); // Idle
        }
    }

    public void AttackEvent()
    {
        pinkStar.attack.attacking = false;
        pinkStar.attack.AttackEven();
    }
}
