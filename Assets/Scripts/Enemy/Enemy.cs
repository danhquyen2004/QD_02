using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityManager
{
    [HideInInspector] public bool lieDown;

    public void StartLie(float time = 1)
    {
        lieDown = true;
        //rb.bodyType = RigidbodyType2D.Static;
        Invoke(nameof(EndLie), time);
    }
    private void EndLie()
    {
        lieDown = false;
        rb.AddForce(Vector3.up * 450);
        //rb.bodyType = RigidbodyType2D.Dynamic;
    }
    public bool CanMove()
    {
        if (stun || dead || lieDown) return false;
        else return true;
    }
}

