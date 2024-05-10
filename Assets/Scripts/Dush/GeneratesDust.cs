using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneratesDust : MonoBehaviour
{
    [SerializeField] private EntityMovement entityMovement;
    [SerializeField] private EntityManager entityManager;
    [SerializeField] private GameObject dustObj;
    [SerializeField] private Transform point;
    [SerializeField] private GameObject newDust;

    private void Start()
    {
        entityMovement = transform.parent.GetComponentInChildren<EntityMovement>();
        entityManager = GetComponentInParent<EntityManager>();
    }
    private void LateUpdate()
    {
        if (entityManager.stun || entityManager.dead) return;
        DustHandling();
    }
    private void DustHandling()
    {
        if (!entityMovement.GroundCheck()) return;

        if (DustRun())
            return;
        if (DustJump())
        {
            StartCoroutine(DustFall());
        }
    }
    private bool DustRun()
    {
        if(entityManager is PlayerManager)
        {
            if (InputManager.Instance.Horizontal != 0)
            {
                if (!entityManager.createdDust)
                {
                    HandlingDustRun();
                    return true;
                }
            }
            return false;
        }
        else if(entityManager is Enemy) 
        {
            EnemyMovement movement = (EnemyMovement)entityMovement;
            if (movement.directionMove != 0)
            {
                if (!entityManager.createdDust)
                {
                    HandlingDustRun();
                    return true;
                }
            }
            return false;
        }

        return false;

    }
    private void HandlingDustRun()
    {
        entityManager.createdDust = true;
        newDust = Instantiate(dustObj, point.position, Quaternion.identity);
        newDust.SetActive(true);
        newDust.transform.localScale = entityManager.transform.localScale;
        newDust.GetComponent<Animator>().Play("Run");
    }
    private bool DustJump()
    {
        if(entityManager is PlayerManager)
        {
            if (InputManager.Instance.Jump)
            {
                ResetDust();
                if (!entityManager.createdDust)
                {
                    entityManager.createdDust = true;
                    newDust = Instantiate(dustObj, point.position, Quaternion.identity);
                    newDust.SetActive(true);
                    newDust.transform.localScale = entityManager.transform.localScale;
                    newDust.GetComponent<Animator>().Play("Jump");
                    return true;
                }
            }
            return false;
        }
        return false;
    }
    private IEnumerator DustFall()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            if (PlayerManager.Instance.movement.GroundCheck())
            {
                ResetDust();
                if (!entityManager.createdDust)
                {
                    PlayerManager.Instance.createdDust = true;
                    newDust = Instantiate(dustObj, point.position, Quaternion.identity);
                    newDust.SetActive(true);
                    newDust.transform.localScale = PlayerManager.Instance.transform.localScale;
                    newDust.GetComponent<Animator>().Play("Fall");

                    //Invoke(nameof(ActiveDustFall), 0.1f);

                }
                break;
            }
            yield return null;
        }
    }
    //private bool DustFall()
    //{
    //    if (!PlayerManager.Instance.createdDust)
    //    {
    //        PlayerManager.Instance.createdDust = true;
    //        newDust = Instantiate(dustObj, point.position, Quaternion.identity);
    //        newDust.SetActive(false);
    //        Invoke(nameof(ActiveDustFall), 0.1f);
    //        return true;
    //    }
    //    return false;
    //}
    public void ActiveDustFall()
    {
        if (newDust != null)
        {
            newDust.SetActive(true);
            newDust.transform.position = point.position;
            newDust.transform.localScale = entityManager.transform.localScale;
            newDust.GetComponent<Animator>().Play("Fall");
        }
    }
    private void ResetDust()
    {
        Destroy(newDust);
        entityManager.createdDust = false;
    }
}