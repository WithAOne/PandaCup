using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWithDamage : MonoBehaviour
{
    public float belowDamage;
    public List<GameObject> parts;

    private StatContainer sc;
    private bool done = false;

    void Start()
    {
        sc = GetComponent<StatContainer>();
    }
    void Update()
    {
        if (!done && sc.Health < belowDamage)
        {
            foreach (GameObject go in parts)
            {
                go.GetComponent<SpriteRenderer>().enabled = false;
            }
            done = true;
        }
    }
}
