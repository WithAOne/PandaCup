using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantDynamic : MonoBehaviour
{
    public LevelLayout layout;

    void Start() { DontDestroyOnLoad(gameObject); }
}
