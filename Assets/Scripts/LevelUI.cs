using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public LevelLayout layout;
    private string scene = "TestingScene";

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
