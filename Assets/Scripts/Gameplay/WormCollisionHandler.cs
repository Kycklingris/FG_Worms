using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormCollisionHandler : MonoBehaviour
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Water"))
        {
            this.SendMessageUpwards("TakeDamage", new DamageClass(1000, 0.0f, Vector3.zero, null));
        }
    }
}
