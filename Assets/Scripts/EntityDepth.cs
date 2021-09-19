using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDepth : MonoBehaviour
{
    void Update()
    {
        // set z to y in order to make higher entities behind lower ones
        var newpos = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        transform.position = newpos;
    }
}
