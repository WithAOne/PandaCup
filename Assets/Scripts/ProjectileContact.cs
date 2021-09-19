using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContact : MonoBehaviour
{
    public Vector2 velocity;
    public float damage = 1;
    public float knockback = 1;

    public float life = 10f;

    void Update()
    {
        life = Mathf.Max(life - Time.deltaTime, 0);
        if (life == 0)
        {
            Destroy(gameObject);
        }
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
            sc.gameObject.SendMessage("DamageTaken", damage); // inform object it's been damaged
        }

        var rb = col.gameObject.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.AddForce(velocity.normalized * knockback * 1000);
        }

        Destroy(gameObject);
    }
}
