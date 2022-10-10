using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
    private CharacterController controller;
    private WormGravity gravityController;
    private float playerSpeed = 4.0f;
    private bool groundedPlayer;
    private float jumpHeight = 1.5f;
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
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        if (Input.GetButtonDown("Jump") && this.groundedPlayer)
        {
            this.gravityController.playerVelocity.y += Mathf.Sqrt(this.jumpHeight * -3.0f * this.gravityValue);
        }

        if (move != Vector3.zero)
        {
            var forward = orbitCamera.forward;
            forward.y = 0;

            var right = Vector3.Cross(forward, Vector3.up);

            var moveForward = forward;
            moveForward *= move.z;

            var moveRight = right;
            moveRight *= -move.x;

            var movement = moveForward + moveRight;

            this.gameObject.transform.forward = movement;
            this.controller.Move(movement * this.playerSpeed * Time.deltaTime);
        }
        
    }
}
