using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    void OnEnable()
    {
        Sniper.RecursiveEnable(this.gameObject.transform);
    }

    static void RecursiveEnable(Transform transform)
    {
        foreach (Transform tran in transform)
        {
            tran.gameObject.SetActive(true);
            Sniper.RecursiveEnable(tran);
        }
    }

    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            this.Shoot();
        }
    }

    void Shoot()
    {

    }
}
