using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public GameObject target;
    public float movementSpeed = 5.0F;
    public float zoomSpeed = 5.0F;
    private Vector3 lastLocalTransform;

    // Start is called before the first frame update
    void Awake()
    {
        this.lastLocalTransform = this.transform.position - this.target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.FollowTarget();
        this.MoveCamera();
        this.ZoomCamera();
        this.SaveLocalTransform();
    }

    void FollowTarget()
    {
        this.transform.position = this.lastLocalTransform + this.target.transform.position;
    }

    void MoveCamera()
    {
        var mouse = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);

        transform.RotateAround(target.transform.position, Vector3.right, mouse.y * movementSpeed * Time.deltaTime);
        transform.RotateAround(target.transform.position, Vector3.up, mouse.x * movementSpeed * Time.deltaTime);

        transform.LookAt(target.transform);
    }

    void ZoomCamera()
    {
        var deltaScroll = Input.GetAxis("Mouse ScrollWheel");

        transform.position -= transform.forward * deltaScroll * zoomSpeed * Time.deltaTime;
    }

    void SaveLocalTransform()
    {
        this.lastLocalTransform = this.transform.position - this.target.transform.position;
    }
}
