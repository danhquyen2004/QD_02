using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] protected float coolDownAtk = 1;
    protected float timer = 1;
    protected bool attacked;

    public bool attacking;  // attacking == true => Cant move;

    private void Start()
    {
        timer = coolDownAtk;
    }
    void Update()
    {
        if (CanAttack())
            Attack();
    }
    private void Attack()
    {
        if (InputManager.Instance.Attack)
        {
            attacked = true;
            attacking = true;
            Debug.Log("Attack");
            PlayerManager.Instance.visual.animator.SetTrigger("AttackTrigger");
        }
    }
    public bool CanAttack()
    {
        if (attacked)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                return false;
            }
            else
            {
                attacked = false;
                timer = coolDownAtk;
                return true;
            }
        }
        else
        {
            return true;
        }
    }

}
