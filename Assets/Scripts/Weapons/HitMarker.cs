using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker : MonoBehaviour
{
    private GameObject attacked;
    [SerializeField] private int damage = 65;
    [SerializeField] private float knockback = 7.5f;

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
