using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContact : MonoBehaviour
{
    public Vector2 velocity;
    public float damage;
    public float knockback;

    void Start()
    {
        
    }
    void FixedUpdate()
    {
        transform.position += new Vector3(velocity.x, velocity.y, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var sc = col.gameObject.GetComponent<StatContainer>();
        if (sc)
        {
            sc.Health -= damage;
        }

        var rb = col.gameObject.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.velocity += velocity.normalized * knockback;
        }

        Destroy(gameObject);
    }
}
