using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    [SerializeField] private EntityManager entity;
    public void DestroyDush()
    {
        entity.createdDust = false;
        Destroy(gameObject);
    }
}
