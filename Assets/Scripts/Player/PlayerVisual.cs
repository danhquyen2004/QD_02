using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public Animator animator;
    public GameObject dust;
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
            if (PlayerManager.Instance.rb.velocity.y <= 0)
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
        if (PlayerManager.Instance.attack.Attack1())
        {
            //animator.SetFloat("AttackState", 1);
            PlayerManager.Instance.attack.attacking = false;
        }
        else
            PlayerManager.Instance.attack.attacking = false;
    }
    public void Attack2Event()
    {
        if (PlayerManager.Instance.attack.Attack2())
        {
            //animator.SetFloat("AttackState", 2);
            PlayerManager.Instance.attack.attacking = false;
        }
        else
        {
            animator.SetFloat("AttackState", 0);
            PlayerManager.Instance.attack.attacking = false;
        }
    }
    public void Attack3Event()
    {
        PlayerManager.Instance.attack.Attack3();
        //animator.SetFloat("AttackState", 0);
        PlayerManager.Instance.attack.attacking = false;
    }
    public void ChangeAttackAnim(float x)
    {
        animator.SetFloat("AttackState", x);
    }
    public void AirAttack1Event()
    {
        PlayerManager.Instance.attack.AirAttack1();
    }
    public void AirAttack2Event()
    {
        PlayerManager.Instance.attack.airAttacking = false;
        PlayerManager.Instance.attack.AirAttack2();
        PlayerManager.Instance.CanFall();
        animator.SetFloat("MoveState", 3); // Fall
        animator.SetFloat("AttackState", 0);
    }
}
