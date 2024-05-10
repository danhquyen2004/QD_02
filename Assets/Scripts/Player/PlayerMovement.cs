using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : EntityMovement
{
    private void Reset()
    {
        Load();
    }
    private void Start()
    {
        Load();
    }
    private void Load()
    {
        pointGroundCheck = transform.Find("GroundCheck").gameObject;
        pointWallCheck = transform.Find("WallCheck").gameObject;
        speed = 6f;
        jumpForce = 700f;
        sizeBoxCheckGround = new Vector2(0.9f, 0.4f);
        sizeBoxCheckWall = new Vector2(1.5f, 1.3f);
    }
    void Update()
    {
        if(!PlayerManager.Instance.attack.attacking)
            Move(InputManager.Instance.Horizontal);
        Jump();
    }

    public override void Move(float direction)
    {
        if (direction < 0)
            transform.parent.localScale = new Vector3(-1,1,1);
        if (direction > 0)
            transform.parent.localScale = new Vector3(1, 1, 1);

        Vector2 velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
        transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed,velocity.y);
    }
    public override void Jump()
    {
        if (GroundCheck())
            if (InputManager.Instance.Jump)
            {
                PlayerManager.Instance.attack.attacking = false;
                PlayerManager.Instance.attack.attacked = false;
                transform.parent.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
            }
                
    }
    public override bool GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pointGroundCheck.transform.position, sizeBoxCheckGround, 0);
        foreach (Collider2D collider in colliders)
        {
            if (LayerMask.LayerToName(collider.gameObject.layer) == "Ground")
                return true;
        }
        return false;
    }
    public override bool WallCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pointWallCheck.transform.position, sizeBoxCheckWall, 0);
        foreach (Collider2D collider in colliders)
        {
            if (LayerMask.LayerToName(collider.gameObject.layer) == "Ground")
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(pointGroundCheck.transform.position, sizeBoxCheckGround);
        Gizmos.DrawWireCube(pointWallCheck.transform.position, sizeBoxCheckWall);
    }
}
