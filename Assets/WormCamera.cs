using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormCamera : MonoBehaviour
{
	private Vector3 lastPosition;
	public GameObject target;
	public float movementSpeed = 5.0F;
	public float zoomSpeed = 5.0F;

	// Start is called before the first frame update
	void Start()
    {
		lastPosition = Input.mousePosition;
	}

    // Update is called once per frame
    void Update() {
		MoveCamera();
		ZoomCamera();
	}

    void MoveCamera()
    {
        var currentPosition = Input.mousePosition;
		var deltaPosition = currentPosition - lastPosition;

		transform.RotateAround(target.transform.position, Vector3.right, deltaPosition.y * movementSpeed * Time.deltaTime);
		transform.RotateAround(target.transform.position, Vector3.up, deltaPosition.x * movementSpeed * Time.deltaTime);

		transform.LookAt(target.transform);

		lastPosition = currentPosition;
    }

    void ZoomCamera()
    {
		var deltaScroll = Input.mouseScrollDelta.y;

		transform.position -= transform.forward * deltaScroll * zoomSpeed * Time.deltaTime;
	}
}
