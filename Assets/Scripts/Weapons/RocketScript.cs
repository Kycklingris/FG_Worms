using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public float forwardVelocity;
    private float gravityValue = -9.81f;
    private Vector3 originalForward;
    private Rigidbody rb;
    public GameObject explosionPrefab;

    public float maxFlightTime = 5.0f;
    private float timeLimit;

    // Start is called before the first frame update
    void Start()
    {
        this.timeLimit = Time.time + this.maxFlightTime;
        this.originalForward = this.transform.forward;
        this.rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var movement = this.originalForward * forwardVelocity * Time.deltaTime;
        movement.y = gravityValue * Time.deltaTime;

        this.rb.MovePosition(transform.position + movement);

        if (Time.time > this.timeLimit)
        {
            this.Complete();
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Water") || collisionInfo.gameObject.CompareTag("WorldObject"))
        {
            this.Complete();
        }
    }

    void Complete()
    {
        this.gravityValue = 0.0f;
        this.forwardVelocity = 0.0f;

        Instantiate(this.explosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
