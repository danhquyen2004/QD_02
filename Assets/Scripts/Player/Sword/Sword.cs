using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction = 1;

    [SerializeField] private Animator animator;

    private bool canPick;
    void Update()
    {
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
    }

    public void SetDir(float dir)
    {
        direction = dir;
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
                    collision.gameObject.GetComponent<EntityManager>().TakeDamage(PlayerManager.Instance.attack.damage);
                }
                Destroy(gameObject);
            }
            else
            {
                if(canPick)
                    Destroy(gameObject);
            }    
        }
    }

}
