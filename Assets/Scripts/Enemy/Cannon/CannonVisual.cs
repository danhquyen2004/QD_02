using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonVisual : MonoBehaviour
{
    public Cannon cannon;
    public Animator animator;
    private void Start()
    {
        cannon = transform.parent.GetComponent<Cannon>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        AnimationHandling();
    }
    private void AnimationHandling()
    {
        if(cannon.dead)
        {
            animator.SetTrigger("DeadTrigger");
            return;
        }
    }

    public void AttackEvent()
    {
        cannon.attack.AttackEven();
    }
}
