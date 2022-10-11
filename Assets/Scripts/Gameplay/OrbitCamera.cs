using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float movementSpeed = 5.0F;
    [SerializeField] private float zoomSpeed = 5.0F;
    private Vector2 rotation;
    private float distance;

    // Start is called before the first frame update
    void Awake()
    {
        this.distance = Vector3.Distance(this.gameObject.transform.position, this.target.position);
        var rotation = this.gameObject.transform.eulerAngles;

        this.rotation = new Vector2(rotation.x, rotation.y);
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = this.target.position;

        this.rotation.x += Input.GetAxis("Mouse Y") * Time.deltaTime * this.movementSpeed;
        this.rotation.y -= Input.GetAxis("Mouse X") * Time.deltaTime * this.movementSpeed;
        this.distance += Input.mouseScrollDelta.y * this.zoomSpeed * Time.deltaTime;

        this.gameObject.transform.rotation = Quaternion.Euler(0, this.rotation.y, 0) * Quaternion.Euler(this.rotation.x, 0, 0);

        this.gameObject.transform.position -= this.gameObject.transform.forward * this.distance;
    }
}
