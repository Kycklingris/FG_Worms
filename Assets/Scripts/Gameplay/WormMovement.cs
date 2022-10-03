using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    public Transform orbitCamera;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
    }

    // Update is called once per frame
    void Update()
    {
        var forward = orbitCamera.forward;
        forward.y = 0;

        var up = new Vector3(0.0f, 1.0f, 0.0f);

        var right = Vector3.Cross(forward.normalized, up.normalized);

        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (move != Vector3.zero)
        {
            var moveForward = forward;
            moveForward *= move.z;

            var moveRight = right;
            moveRight *= -move.x; // Not entirely sure why, but anyways

            gameObject.transform.forward = forward;
            controller.Move(moveForward * playerSpeed * Time.deltaTime);
            controller.Move(moveRight * playerSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
