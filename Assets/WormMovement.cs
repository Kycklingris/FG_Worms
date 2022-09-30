using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
    private CharacterController controller;
    // private Vector3 playerVelocity;
    // private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    // private float jumpHeight = 1.0f;
    // private float gravityValue = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
    }

    // Update is called once per frame
    void Update()
    {

        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        controller.Move(move * playerSpeed * Time.deltaTime);
    }
}
