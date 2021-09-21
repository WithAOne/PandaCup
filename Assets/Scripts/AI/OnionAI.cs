using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2020 Isabella Dare/WithAOne

public class OnionAI : MonoBehaviour
{
    Rigidbody2D rb;
    Animator ani;

    public GameObject visual;

    // runs to random places on the screen to run away and attacks every now and again
    public Vector2 loc; // where to go
    public float moveSpeed = 0.5f;
    public Vector2 limits;
    public Vector2 centre;

    private float relocateTimer = 0f;
    public float relocateInterval = 10f; // in seconds

    private bool moving = true;
    private float lead = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = visual.GetComponent<Animator>();
    }
    void Update()
    {
        // relocate every now and then
        if (relocateTimer == 0)
        {
            var cp = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
            loc = new Vector2(centre.x + Random.Range(-limits.x, limits.x), -centre.y + Random.Range(-limits.y, limits.y)) + cp;
            moving = true;

            relocateTimer = relocateInterval;
        }
        relocateTimer = Mathf.Max(relocateTimer - Time.deltaTime, 0);

        // move towards location
        ani.SetBool("Walking", moving);
        if (moving)
        {
            // figure out motion vector then move
            var dir = -(new Vector2(transform.position.x, transform.position.y) - loc).normalized;
            rb.velocity += dir * Time.deltaTime * moveSpeed * 100;

            // face in the correct direction
            var s = visual.transform.localScale;
            if (dir.x > 0)
            { visual.transform.localScale = new Vector3(Mathf.Abs(s.x), s.y, s.z); }
            else if (dir.x < 0)
            { visual.transform.localScale = new Vector3(-Mathf.Abs(s.x), s.y, s.z); }

            // if close enough, stop moving
            if (Vector2.Distance(transform.position, loc) <= lead)
            { moving = false; }
        }
    }
}
