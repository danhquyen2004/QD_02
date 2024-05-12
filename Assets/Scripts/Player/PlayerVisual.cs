using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public Animator animator;
    public GameObject dust;
    private bool hit;
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
        if (PlayerManager.Instance.dead)
        {
            animator.SetTrigger("DeadTrigger");
            return;
        }
        if (PlayerManager.Instance.attack.holdingSword)
        {
            PlayerManager.Instance.visual.SetLayerWithoutSword(0);
        }
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
            hit = true;
            PlayerManager.Instance.attack.attacking = false;
        }
        else
        {
            hit = false;
            PlayerManager.Instance.attack.attacking = false;
        }
    }
    public void Attack2Event()
    {
        if (PlayerManager.Instance.attack.Attack2())
        {
            hit = true;
            PlayerManager.Instance.attack.attacking = false;
        }
        else
        {
            hit = false;
            animator.SetFloat("AttackState", 0);
            PlayerManager.Instance.attack.attacking = false;
        }
    }
    public void Attack3Event()
    {
        if(!PlayerManager.Instance.attack.Attack3())
            hit = false;
        PlayerManager.Instance.attack.attacking = false;
    }
    public void ThrowSwordEven()
    {
        PlayerManager.Instance.attack.InstantiateSword();
    }
    public void SetLayerWithoutSword(int state)
    {
        animator.SetLayerWeight(animator.GetLayerIndex("WithoutSword"), state);
    }    
    public void ChangeAttackAnim(float x)
    {
        if(x!=0)
        {
            if (hit)
            {
                animator.SetFloat("AttackState", x);
            }    
        }    
        else
            animator.SetFloat("AttackState", x);
    }
    public void AirAttack1Event()
    {
        PlayerManager.Instance.attack.AirAttack1();
    }
    public void AirAttack2Event()
    {
        PlayerManager.Instance.attack.AirAttack2();
    }
    public void FinishAirAttack()
    {
        PlayerManager.Instance.CanFall();
        animator.SetFloat("MoveState", 3); // Fall
        animator.SetFloat("AttackState", 0);
    }
}
