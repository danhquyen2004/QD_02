using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneratesDust : MonoBehaviour
{
    [SerializeField] private EntityMovement entityMovement;
    [SerializeField] private EntityManager entityManager;
    [SerializeField] private Dust dustObj;
    [SerializeField] private Transform point;
    [SerializeField] private Dust newDust;

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
        if (entityManager is PlayerManager)
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
        else if (entityManager is Enemy)
        {
            if (entityManager.rb.velocity.x != 0)
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
        newDust = ObjectPoolingDust.Instant.GetObjectType(dustObj);
        newDust.transform.position = point.position;
        newDust.gameObject.SetActive(true);
        newDust.transform.localScale = entityManager.transform.localScale;
        newDust.GetComponent<Animator>().Play("Run");
    }
    private bool DustJump()
    {
        if (entityManager is PlayerManager)
        {
            if (InputManager.Instance.Jump)
            {
                ResetDust();
                if (!entityManager.createdDust)
                {
                    entityManager.createdDust = true;
                    newDust = ObjectPoolingDust.Instant.GetObjectType(dustObj);
                    newDust.transform.position = point.position;
                    newDust.gameObject.SetActive(true);
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
                    newDust = ObjectPoolingDust.Instant.GetObjectType(dustObj);
                    newDust.transform.position = point.position;
                    newDust.gameObject.SetActive(true);
                    newDust.transform.localScale = PlayerManager.Instance.transform.localScale;
                    newDust.GetComponent<Animator>().Play("Fall");

                    //Invoke(nameof(ActiveDustFall), 0.1f);

                }
                break;
            }
            yield return null;
        }
    }
    public void ActiveDustFall()
    {
        if (newDust != null)
        {
            newDust.gameObject.SetActive(true);
            newDust.transform.position = point.position;
            newDust.transform.localScale = entityManager.transform.localScale;
            newDust.GetComponent<Animator>().Play("Fall");
        }
    }
    private void ResetDust()
    {
        newDust.gameObject.SetActive(false);
        entityManager.createdDust = false;
    }
}