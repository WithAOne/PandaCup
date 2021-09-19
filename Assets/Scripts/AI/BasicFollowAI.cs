using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFollowAI : MonoBehaviour
{
    Rigidbody2D rb;
    AIContainer aicon;

    public GameObject visual;

    public bool canMove = true;

    public float tailDistance = 3; // how close to get to the target
    public float moveSpeed = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aicon = GetComponent<AIContainer>();
    }
    void FixedUpdate()
    {
        // acquire target from ai variable container
        var target = aicon.target;

        // move towards target
        if (canMove && tailDistance > 0 && Vector3.Distance(transform.position, target.transform.position) > tailDistance)
        {
            // if the player is still active (has it's rigidbody enabled
            if (target.GetComponent<Rigidbody2D>())
            {
                var dir = (target.GetComponent<Rigidbody2D>().position - rb.position).normalized;
                rb.velocity += dir * moveSpeed;

                // face in the correct direction
                var s = visual.transform.localScale;
                if (dir.x > 0)
                { visual.transform.localScale = new Vector3(Mathf.Abs(s.x), s.y, s.z); }
                else if (dir.x < 0)
                { visual.transform.localScale = new Vector3(-Mathf.Abs(s.x), s.y, s.z); }
            }
        }
    }
}
