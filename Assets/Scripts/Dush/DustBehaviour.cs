using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    [SerializeField] private EntityManager entity;
    private void Reset()
    {
        entity = transform.parent.GetComponentInParent<EntityManager>();
    }
    public void DestroyDush()
    {
        entity.createdDust = false;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
