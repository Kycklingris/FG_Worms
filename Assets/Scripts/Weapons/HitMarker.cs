using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker : MonoBehaviour
{
    private GameObject attacked;
    public int damage = 65;
    public float knockback = 7.5f;

    public void Attack(GameObject target, Vector3 direction)
    {
        this.attacked = target;

        target.transform.parent.gameObject.SendMessage("TakeDamage", new DamageClass(this.damage, this.knockback, direction, this.gameObject));
    }

    void OnLanded(GameObject other)
    {
        Destroy(this.gameObject);
        GameObject.Find("GameplayController").GetComponent<GameplayScript>().NextTurn();
    }
}
