using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2020 Isabella Dare/WithAOne

public class RangedAI : MonoBehaviour
{
    Rigidbody2D rb;
    AIContainer aicon;
    Animator ani;
    BasicFollowAI fai;

    public GameObject visual;

    public GameObject throwProjectile;
    public string throwAnimation;
    public float maxDist = 30f;
    public float throwInterval = 5f;
    public float projectileSpeed = 0.1f;

    private float throwTimer = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aicon = GetComponent<AIContainer>();
        ani = visual.GetComponent<Animator>();
        fai = GetComponent<BasicFollowAI>();
        throwTimer = Random.Range(throwInterval-1, throwInterval+1);
    }
    void Update()
    {
        // acquire target from ai variable container
        var target = aicon.target;

        // throw on interval
        throwTimer = Mathf.Max(0, throwTimer - Time.deltaTime);
        if (throwTimer == 0)
        {
            // create projectile and hurl it in correct direction
            GameObject proj = Instantiate(throwProjectile, transform.position, new Quaternion());
            proj.GetComponent<ProjectileContact>().velocity = (target.GetComponent<Rigidbody2D>().position - rb.position).normalized * projectileSpeed;

            // play throw animation
            ani.Play(throwAnimation);

            // reset timer
            throwTimer = Random.Range(throwInterval-1, throwInterval+1);
        }

        // don't move while throwing
        if (ani.GetCurrentAnimatorStateInfo(0).IsName(throwAnimation))
        {
            fai.canMove = false;
        }
        else
        {
            fai.canMove = true;
        }
    }

    public void DamageTaken(float damage)
    {
        // if damaged, stun and reset throw timer
        if (damage > 0)
        {
            throwTimer = throwInterval + 1f;
        }
    }
}
