using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction = 1;
    private float oldDir;

    [SerializeField] private Animator animator;

    public bool canPick;
    public float maxDistance;
    void Update()
    {
        SetOldDirForSword();
        CheckMaxDistance();
        SetTriggerForSword();
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
    }

    public void SetDir(float dir)
    {
        direction = dir;
        oldDir = dir;
        transform.localScale = new Vector3(dir, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Ground")
        {
            direction = 0;
            animator.Play("Embedded");
            canPick = true;
        }
        else
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) != "Player")
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    //Do some thing
                    PlayerPickSword();
                    collision.gameObject.GetComponent<EntityManager>().TakeDamage(PlayerManager.Instance.attack.damage);
                }
            }
            else
            {
                if (canPick)
                {
                    PlayerPickSword();
                }
            }
        }
    }
    private void SetTriggerForSword()
    {
        RaycastHit2D[] raycastHit2Ds = Physics2D.RaycastAll(transform.position, Vector2.right * direction, 1);
        Debug.DrawRay(transform.position, Vector2.right * direction * 1);
        foreach(RaycastHit2D ray in raycastHit2Ds)
        {
            if(ray.collider.gameObject.tag == "OneWay")
            {
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                return;
            }
        }
    }
    private void SetOldDirForSword()
    {
        if(gameObject.GetComponent<BoxCollider2D>().isTrigger == true)
        {
            direction = oldDir;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
    private void PlayerPickSword()
    {
        PlayerManager.Instance.attack.holdingSword = true;
        Destroy(gameObject);
    }
    private void CheckMaxDistance()
    {
        if(Vector2.Distance(transform.position, PlayerManager.Instance.transform.position) > maxDistance)
        {
            PlayerPickSword();
        }
    }
}
