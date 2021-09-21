using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCloud : MonoBehaviour
{
    RectTransform rt;
    public float speed = 1f;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }
    void Update()
    {
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x - (speed * Time.deltaTime), rt.anchoredPosition.y);
        if (rt.anchoredPosition.x < -1300f)
        {
            rt.anchoredPosition = new Vector2(1300f, rt.anchoredPosition.y);
        }
    }
}
