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

            if (!value)
            {
                this.SetWeapon(-1);
            }
        }
    }

    private WormMovement movement;
    private WormKnockback knockback;
    private Transform[] weapons;
    private GameObject orbitCamera;

    public int health = 100;

    void Awake()
    {
        this.movement = gameObject.GetComponentInChildren(typeof(WormMovement)) as WormMovement;
        this.knockback = gameObject.GetComponentInChildren(typeof(WormKnockback)) as WormKnockback;
        var character = gameObject.transform.Find("Character");
        this.orbitCamera = this.transform.Find("Orbit Camera").gameObject;

        var weaponSlot = character.transform.Find("Weapon Slot");
        this.weapons = Array.FindAll(weaponSlot.GetComponentsInChildren<Transform>(), child => child != weaponSlot);

        this.SetWeapon(-1);

        this.isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isActive)
        {
            return;
        } 

        if (Input.GetButtonDown("Weapon1"))
        {
            this.SetWeapon(0);
        } else if (Input.GetButtonDown("Weapon2"))
        {
            this.SetWeapon(1);
        }
    }

    void SetWeapon(int number)
    {
        var exitBeforeSelect = false;
        if (number < 0)
        {
            exitBeforeSelect = true;
        } else if (this.weapons[number].gameObject.activeSelf) // If the same button has been pressed twice, select no weapon
        {
            exitBeforeSelect = true;
        }

        foreach (var child in this.weapons) // Disable all weapons
        {
            child.gameObject.SetActive(false);
        }

        if (exitBeforeSelect)
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

        if (this.health <= 0)
        {
            this.KillSelf();
        }
    }

    void KillSelf()
    {
        var gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayScript>();

        gameplayController.Kill(this);
    }

    void Death()
    {
        this.isActive = false;
        Destroy(this.gameObject);
    }

    public void SetMaterial(Material material)
    {
        var character = this.transform.Find("Character");
        character.GetComponent<Renderer>().material = material;
        character.transform.Find("Cube").GetComponent<Renderer>().material = material;
    }
}
