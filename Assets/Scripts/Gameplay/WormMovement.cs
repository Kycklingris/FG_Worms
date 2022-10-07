using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
    private CharacterController controller;
    private WormGravity gravityController;
    private float playerSpeed = 2.0f;
    private bool groundedPlayer;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public Transform orbitCamera;

    // Start is called before the first frame update
    void Awake()
    {
        this.controller = this.gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
        this.gravityController = this.gameObject.GetComponent(typeof(WormGravity)) as WormGravity;
    }

    // Update is called once per frame
    void Update()
    {
        this.groundedPlayer = controller.isGrounded;
        var forward = orbitCamera.forward;
        forward.y = 0;

        var up = new Vector3(0.0f, 1.0f, 0.0f);

        var right = Vector3.Cross(forward.normalized, up.normalized);

        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (move != Vector3.zero)
        {
            var moveForward = forward;
            moveForward *= move.z;

            var moveRight = right;
            moveRight *= -move.x; // Not entirely sure why it needs to be negative, guessing the input is mapped as A/left to be positive

            var movement = moveForward + moveRight;

            this.gameObject.transform.forward = movement;
            this.controller.Move(movement * this.playerSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && this.groundedPlayer)
        {
            this.gravityController.playerVelocity.y += Mathf.Sqrt(this.jumpHeight * -3.0f * this.gravityValue);
        }
    }
}
