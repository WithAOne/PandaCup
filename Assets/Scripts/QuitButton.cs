using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        Application.Quit();
    }
}
