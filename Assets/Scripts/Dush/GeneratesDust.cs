using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneratesDust : MonoBehaviour
{
    [SerializeField] private GameObject dustObj;
    [SerializeField] private Transform point;
    [SerializeField] private GameObject newDust;

    private void LateUpdate()
    {
        DustHandling();
    }
    private void DustHandling()
    {
        if (!PlayerManager.Instance.movement.GroundCheck()) return;
        if (DustRun())
            return;
        if (DustJump())
            return;
        if (DustFall())
            return;
    }
    private bool DustRun()
    {
        if (InputManager.Instance.Horizontal != 0)
        {
            if (!PlayerManager.Instance.createdDust)
            {
                PlayerManager.Instance.createdDust = true;
                newDust = Instantiate(dustObj, point.position, Quaternion.identity);
                newDust.SetActive(true);
                newDust.transform.localScale = PlayerManager.Instance.transform.localScale;
                newDust.GetComponent<Animator>().Play("Run");
                return true;
            }
        }
        return false;

    }
    private bool DustJump()
    {
        if(InputManager.Instance.Jump)
        {
            if (!PlayerManager.Instance.createdDust)
            {
                PlayerManager.Instance.createdDust = true;
                newDust = Instantiate(dustObj, point.position, Quaternion.identity);
                newDust.SetActive(true);
                newDust.transform.localScale = PlayerManager.Instance.transform.localScale;
                newDust.GetComponent<Animator>().Play("Jump");
                return true;
            }
        }
        return false;
    }
    private bool DustFall()
    {
        if (InputManager.Instance.Jump)
        {
            if (!PlayerManager.Instance.createdDust)
            {
                PlayerManager.Instance.createdDust = true;
                newDust = Instantiate(dustObj, point.position, Quaternion.identity);
                newDust.SetActive(false);
                Invoke(nameof(ActiveDustFall), 0.1f);
                return true;
            }
        }
        return false;
    }
    public void ActiveDustFall()
    {
        if (PlayerManager.Instance.movement.GroundCheck())
        {
            if (newDust != null)
            {
                newDust.SetActive(true);
                newDust.transform.position = point.position;
                newDust.transform.localScale = PlayerManager.Instance.transform.localScale;
                newDust.GetComponent<Animator>().Play("Fall");
            }
        }
    }
}