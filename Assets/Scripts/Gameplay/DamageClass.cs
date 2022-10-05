using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageClass
{
    public int damage;
    public float knockback;
    public Vector3 direction;
    public GameObject origin;

    public DamageClass(int damage, float knockback, Vector3 direction, GameObject origin)
    {
        this.damage = damage;
        this.knockback = knockback;
        this.direction = direction;
        this.origin = origin;
    }
}
