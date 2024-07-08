using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingDust : ObjectPoolingX<Dust>
{
    public override Dust GetObjectType(Dust key)
    {
        Dust dust = base.GetObjectType(key);
        dust.transform.parent = transform;
        return dust;
    }
}
