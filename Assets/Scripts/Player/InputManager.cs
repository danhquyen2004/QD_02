using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }


    private float horizontal;
    public float Horizontal { get { return horizontal; } }

    private bool jump;
    public bool Jump { get { return jump; } }

    private bool attack;
    public bool Attack { get { return attack; } }


    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        HorizontalInput();
        JumpInput();
        AttackInput();
    }
    private void HorizontalInput()
    {
        horizontal = Input.GetAxis("Horizontal");
    }
    private void JumpInput()
    {
        jump = Input.GetKeyDown(KeyCode.Space);
    }
    private void AttackInput()
    {
        attack = Input.GetKeyDown(KeyCode.J);
    }
}
