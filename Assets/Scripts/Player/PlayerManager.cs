using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerManager : EntityManager
{
    private static PlayerManager instance;
    public static PlayerManager Instance { get { return instance; } }

    public PlayerVisual visual;
    public PlayerMovement movement;
    public PlayerAttack attack;

    protected override void Start()
    {
        base.Start();
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
}