using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EntityMovement
{
    private Enemy m_enemy;
    public int directionMove = 1;
    [SerializeField] protected GameObject pointChangeDirCheck;

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
        m_enemy = transform.parent.GetComponent<Enemy>();
        pointGroundCheck = transform.Find("GroundCheck").gameObject;
        pointWallCheck = transform.Find("WallCheck").gameObject;
    }
    void Update()
    {
        if (m_enemy.stun || m_enemy.dead) return;

        if (ChangeDirectionCheck() && GroundCheck())
            directionMove *= -1;
        Move(directionMove);
    }

    public override void Move(float direction)
    {
        if (direction > 0)
            transform.parent.localScale = new Vector3(1, 1, 1);
        if (direction < 0)
            transform.parent.localScale = new Vector3(-1, 1, 1);

        Vector2 velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
        transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, velocity.y);
    }
    public override void Jump()
    {
        if (GroundCheck())
            if (InputManager.Instance.Jump)
                transform.parent.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
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

    private bool ChangeDirectionCheck()
    {
        Debug.DrawRay(pointChangeDirCheck.transform.position, Vector3.down);
        RaycastHit2D colliderCheckGround = Physics2D.Raycast(pointChangeDirCheck.transform.position, Vector3.down, 1, LayerMask.GetMask("Ground"));
        Debug.DrawRay(pointChangeDirCheck.transform.position, Vector3.left * transform.parent.localScale.x);
        RaycastHit2D collidersCheckWall = Physics2D.Raycast(pointChangeDirCheck.transform.position, Vector3.left * transform.parent.localScale.x, 1, LayerMask.GetMask("Ground"));

        if (colliderCheckGround.collider == null)
        {
            return true;
        }
        if (collidersCheckWall.collider != null)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(pointGroundCheck.transform.position, sizeBoxCheckGround);
        Gizmos.DrawWireCube(pointWallCheck.transform.position, sizeBoxCheckWall);
    }
}
