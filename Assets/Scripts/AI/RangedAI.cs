using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAI : MonoBehaviour
{
    Rigidbody2D rb;
    AIContainer aicon;

    public GameObject visual;

    public float maxDist = 30f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aicon = GetComponent<AIContainer>();
    }
    void Update()
    {
        // acquire target from ai variable container
        var target = aicon.target;


    }
}
