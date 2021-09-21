using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2020 Isabella Dare/WithAOne

public class LevelUI : MonoBehaviour
{
    public LevelLayout layout;
    private string scene = "Level";

    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        var go = new GameObject("PersistantDynamic");
        go.AddComponent<PersistantDynamic>();
        go.GetComponent<PersistantDynamic>().layout = layout;

        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
