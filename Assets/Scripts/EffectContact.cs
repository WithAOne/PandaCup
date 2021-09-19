using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectContact : MonoBehaviour
{
    Collider2D collider;

    public GameObject owner;
    public float life = 0;
    public float damage = 0;
    public float knockback = 0;
    public GameObject contactFrom;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    void Update()
    {
        // enact effects
        Fire();

        // update life timer and remove self if life is over
        life -= Time.deltaTime;
        if (life <= 0) { Destroy(gameObject); }
    }

    void Fire()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x);
        foreach (Collider2D c in cols)
        {
            // only do anything if it's not hitting it's owner
            if (owner == null || c.gameObject.GetInstanceID() != owner.GetInstanceID())
            {
                // push the object away if the object has a rigidbody
                Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // new position
                    var npos = transform.position;
                    if (contactFrom != null) { npos = contactFrom.transform.position; }
                    // direction
                    var dir = (rb.transform.position - npos).normalized * knockback * 1000;
                    rb.AddForceAtPosition(new Vector2(dir.x, dir.y), transform.position);
                }

                // deal damage to health if the object has a stat container
                StatContainer sc = c.GetComponent<StatContainer>();
                if (sc != null)
                {
                    sc.Health -= damage;
                    c.SendMessage("DamageTaken", damage); // inform object it's been damaged
                }
            }
        }
    }
}
