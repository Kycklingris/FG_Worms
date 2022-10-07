using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormGravity : MonoBehaviour
{
    private CharacterController controller;
    public Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;


    // Start is called before the first frame update
    void Awake()
    {
        this.controller = gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
    }

    // Update is called once per frame
    void Update()
    {
        this.groundedPlayer = controller.isGrounded;

        if (this.groundedPlayer && this.playerVelocity.y < 0)
        {
            this.playerVelocity.y = 0;
        }

        this.playerVelocity.y += gravityValue * Time.deltaTime;
        this.controller.Move(playerVelocity * Time.deltaTime);
    }
}
