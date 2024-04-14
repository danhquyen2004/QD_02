using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        AnimationHandling();
    }
    private void AnimationHandling()
    {
        if (!PlayerManager.Instance.movement.GroundCheck())
        {
            if (PlayerManager.Instance.rb.velocity.y < 0)
                animator.SetFloat("MoveState", 3); // Fall
            else
                animator.SetFloat("MoveState", 2); // Jump
            return;
        }

        if (InputManager.Instance.Horizontal != 0)
            animator.SetFloat("MoveState", 1); // Run
        else
            animator.SetFloat("MoveState", 0); // Idle
    }
    public void Attack1Event()
    {
        PlayerManager.Instance.attack.attacking = false;
    }
}
