using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropController : MonoBehaviour
{
    public List<GameObject> objects;
    public List<GameObject> clouds;
    public GameObject tree;
    public GameObject cloud1;
    public GameObject cloud2;

    void Start()
    {
        // NewBackdrop(-10f, GetComponent<GlobalDynamic>().cameraLimit.y + 10f);
    }
    void Update()
    {
        //UpdateList(objects);
        //UpdateList(clouds);

    }

    public void NewBackdrop(float startx, float endx)
    {
        float difx = endx - startx;

        // create trees
        float treedisting = 15f; // rough distance between trees
        float treecount = difx / treedisting;
        for (int i = 0; i < treecount; i++)
        {
            float x = (treedisting * i) + Random.Range(1f, treedisting-1f);
            GameObject obj = Instantiate(tree);
            obj.transform.position = new Vector3(startx + x, obj.transform.position.y, obj.transform.position.z);
        }

        // create trees
        float clouddisting = 5f; // rough distance between trees
        float cloudcount = difx / clouddisting;
        for (int i = 0; i < cloudcount; i++)
        {
            float x = Random.Range(startx, endx);
            GameObject obj;
            
            if (Random.Range(0, 1f) < 0.5f)
            { obj = Instantiate(cloud1); }
            else
            { obj = Instantiate(cloud2); }

            obj.transform.position = new Vector3(x, obj.transform.position.y, obj.transform.position.z);
        }


    }

    void UpdateList(List<GameObject> list)
    {
        foreach (GameObject obj in list)
        {
            if (Mathf.Abs(obj.transform.transform.position.x - Camera.main.transform.position.x) > 30 && obj.transform.position.x < Camera.main.transform.position.x)
            {
                Destroy(obj);
            }
        }
    }
}
