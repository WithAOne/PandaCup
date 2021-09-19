using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    StatContainer sc;

    public GameObject bar;
    public GameObject statcon;

    float fullSize;
    float fullAmount;

    void Start()
    {
        sc = statcon.GetComponent<StatContainer>();
        fullSize = bar.GetComponent<RectTransform>().sizeDelta.x;
        fullAmount = sc.Health;
    }
    void Update()
    {
        var rect = bar.GetComponent<RectTransform>().rect;
        bar.GetComponent<RectTransform>().sizeDelta = new Vector2(fullSize * (sc.Health / fullAmount), rect.height);
        //bar.GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width - 0.1f, rect.height);
        //bar.GetComponent<RectTransform>().rect.Set(rect.x, rect.y, rect.width - 1, rect.height);
    }
}
