using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    private float startHeld = 0.0f;
    public GameObject rocketPrefab;
    public Transform cameraTransform;

    // Update is called once per frame
    void Update()
    {
        var cameraRot = this.cameraTransform.eulerAngles.x;

        this.gameObject.transform.localRotation = Quaternion.Euler(cameraRot - 45.0f, 0, 0);

        if (Input.GetButtonDown("Fire1") && this.startHeld == 0.0)
        {
            this.startHeld = Time.time;
            return;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            var time = Time.time - this.startHeld;
            this.startHeld = 0.0f;
            this.Shoot(time);
        }
    }

    void Shoot(float time)
    {
        if (time > 3.0f) // Cap power level
        {
            time = 3.0f;
        }

        time = Mathf.Pow(time, 2.0f);

        time *= 10.0f;

        var location = this.transform.position;

        var forward = this.transform.rotation * Vector3.up;

        location += forward * 1.0f;

        var projectile = Instantiate(this.rocketPrefab, location, Quaternion.identity);
        projectile.transform.forward = forward;

        var rocketController = projectile.GetComponent(typeof(RocketScript)) as RocketScript;

        rocketController.forwardVelocity = time;

        this.SendMessageUpwards("SentShot");
    }
}
