using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBehaviourController : MonoBehaviour
{
    private bool _isActive = false;
    public bool isActive
    {
        get => _isActive;
        set
        {
            this.movement.enabled = value;
            this.orbitCamera.SetActive(value);
            this._isActive = value;
        }
    }

    private WormMovement movement;
    private WormKnockback knockback;
    private Transform[] weapons;
    private GameObject orbitCamera;

    public int health = 100;

    void Start()
    {
        this.movement = gameObject.GetComponentInChildren(typeof(WormMovement)) as WormMovement;
        this.knockback = gameObject.GetComponentInChildren(typeof(WormKnockback)) as WormKnockback;
        var character = gameObject.transform.Find("Character");
        this.orbitCamera = this.transform.Find("Orbit Camera").gameObject;

        var weaponSlot = character.transform.Find("Weapon Slot");
        this.weapons = Array.FindAll(weaponSlot.GetComponentsInChildren<Transform>(), child => child != weaponSlot);

        this.SetWeapon(-1);

        this.isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Weapon1"))
        {
            this.SetWeapon(0);
        } else if (Input.GetButtonDown("Weapon2"))
        {

        }
    }

    void SetWeapon(int number)
    {
        foreach (var child in this.weapons)
        {
            child.gameObject.SetActive(false);
        }

        if (number < 0)
        {
            return;
        }
        
        this.weapons[number].gameObject.SetActive(true);
    }

    void SentShot()
    {
        this.isActive = false;
    }

    void TakeDamage(DamageClass damage)
    {
        if (damage.knockback != 0.0f)
        {
            this.knockback.Knockback(damage);
        }

        this.health -= damage.damage;


    }
}
