using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float range = 12.0f;
    public int damage = 45;
    public string targetTag = "Worm";
    public float time = 0.5f;
    public float knockback = 20.0f;
    private float creationTime;
    private float scaleStep;
    private List<GameObject> attacked = new List<GameObject>();

    void Awake()
    {
        this.scaleStep = (this.range - 1) / this.time;
        this.creationTime = Time.time;
    }

    void FixedUpdate()
    {
        if (Time.time > creationTime + this.time)
        {
            if (this.attacked.Count == 0)
            {
                Destroy(this.gameObject);
                GameObject.Find("GameplayController").GetComponent<GameplayScript>().NextTurn();
            }
        } else
        {
            this.transform.localScale += new Vector3(this.scaleStep * Time.deltaTime, this.scaleStep * Time.deltaTime, this.scaleStep * Time.deltaTime);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(this.targetTag))
        {
            foreach (var obj in this.attacked)
            {
                if (obj == other.gameObject)
                {
                    return;
                }
            }

            this.attacked.Add(other.gameObject);

            var direction = (other.gameObject.transform.position - this.transform.position).normalized;

            other.gameObject.transform.parent.gameObject.SendMessage("TakeDamage", new DamageClass(this.damage, this.knockback, direction, this.gameObject));
        }
        
    }

    void OnLanded(GameObject other)
    {
        this.attacked.Remove(other.gameObject);

        if (this.attacked.Count == 0)
        {
            Destroy(this.gameObject);
            GameObject.Find("GameplayController").GetComponent<GameplayScript>().NextTurn();
        }
    }
}
