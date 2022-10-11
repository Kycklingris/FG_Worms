using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeCamera : MonoBehaviour
{
    private Vector2 rotation = new Vector2(0.0f, 90.0f);
    [SerializeField] private float sensitivity = 400f;
    [SerializeField] private Transform worm;

    void OnEnable()
    {
        //this.rotation.x = this.worm.rotation.y;
    }

    // Update is called once per frame
    // Kind of, although it rotates horizontally differently https://gist.github.com/KarlRamstedt/407d50725c7b6abeaf43aee802fdd88e
    void Update()
    { 
        this.rotation.x += Input.GetAxis("Mouse X") * this.sensitivity * Time.deltaTime;
        this.rotation.y += Input.GetAxis("Mouse Y") * this.sensitivity * Time.deltaTime;
        this.rotation.y = Mathf.Clamp(this.rotation.y, 90f - 89f, 90f + 89f);

        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);

        this.worm.rotation = xQuat;
        this.transform.localRotation = yQuat;
    }
}
