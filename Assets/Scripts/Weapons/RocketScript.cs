using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public float forwardVelocity;
    private float gravityValue = -9.81f;
    private Rigidbody rb;
    public GameObject explosionPrefab;
    private Vector3 velocity;

    public float maxFlightTime = 5.0f;
    private float timeLimit;

    // Start is called before the first frame update
    void Start()
    {
        this.timeLimit = Time.time + this.maxFlightTime;
        this.rb = this.GetComponent<Rigidbody>();

        this.velocity = this.transform.forward * forwardVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.velocity.y += this.gravityValue * Time.deltaTime;

        this.rb.MovePosition(transform.position + this.velocity * Time.deltaTime);

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
