using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropObject : MonoBehaviour
{
    public float depth = 0.2f;
    public float depthVariety = 0.1f;
    public float x;
    public float startx;
    public float xdrift = 0f;
    public float xdriftVariety;
    public float yVariety = 0.25f;

    void Start()
    {
        depth += Random.Range(-depthVariety / 2, depthVariety / 2);
        xdrift += Random.Range(0, xdriftVariety);
        transform.position = new Vector3(transform.position.x/depth, transform.position.y + Random.Range(-yVariety / 2, yVariety / 2), transform.position.z);
        x = -transform.position.x;
    }
    void Update()
    {
        x = x + xdrift;
        transform.position = new Vector3((x + Camera.main.transform.position.x) * -depth, transform.position.y, transform.position.z);
    }
}
