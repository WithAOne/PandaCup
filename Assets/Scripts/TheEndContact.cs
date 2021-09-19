using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEndContact : MonoBehaviour
{
    public string scene;
    public GameObject agent;

    [System.NonSerialized] public bool ending = false;
    private float endTimer = 3f; // seconds before level ends after activated

    void Update()
    {
        if (ending)
        {
            endTimer = Mathf.Max(0, endTimer - Time.deltaTime);

            if (endTimer == 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetInstanceID() == agent.GetInstanceID())
        {
            agent.GetComponent<CharacterControl>().enabled = false;
            ending = true;
            //UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }
    }
}
