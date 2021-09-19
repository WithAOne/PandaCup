using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDynamic : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject encounterObject;
    public GameObject sceneObject;
    public string menu;

    [Header("Level")]
    public LevelLayout layout;
    [System.NonSerialized] public List<GameObject> currentEncounterItems = new List<GameObject>();
    [System.NonSerialized] public int currentEncounter = 0;

    [Header("Camera")]
    public float cameraAhead = 6f;
    public float ease = 10;
    private float cameraStart;
    public Vector2 cameraLimit;

    [Header("Colliders")]
    public GameObject topCollider;
    public GameObject bottomCollider;

    private float roughHalfCamSize = 4f; //17.5f;

    void Start()
    {
        cameraStart = Camera.main.gameObject.transform.position.x;
        cameraLimit = new Vector2(cameraStart, 0);

        // get any carryover layout
        var pd = GameObject.Find("PersistantDynamic");
        if (pd != null)
        {
            layout = pd.GetComponent<PersistantDynamic>().layout;
            Destroy(pd);
        }

        // start scene creation
        AdvanceEncounter();
    }
    void Update()
    {
        Vector3 pl = playerObject.transform.position;
        float goal = pl.x;

        // only if moving
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            // make camera slightly ahead the player
            float side = playerObject.GetComponent<CharacterControl>().face;
            goal = pl.x + (cameraAhead * side);
        }

        // move camera to desired position
        var camt = Camera.main.gameObject.transform;
        var changex = (goal - camt.position.x) / ease;
        var newx = Mathf.Min(Mathf.Max(camt.position.x + changex, cameraLimit.x), cameraLimit.y);
        camt.position = new Vector3(newx, camt.position.y, camt.position.z);


        // check on current encounter
        // remove dead items from the list
        List<GameObject> gos = new List<GameObject>();
        foreach (GameObject item in currentEncounterItems)
        { if (item == null) { gos.Add(item); } }
        foreach (GameObject go in gos)
        { currentEncounterItems.Remove(go); }

        // when encounter is finished
        if (currentEncounterItems.Count == 0)
        {
            // if this is the last encounter
            if (!AdvanceEncounter())
            {
                playerObject.GetComponent<CharacterControl>().visual.GetComponent<Animator>().Play("Win");
                TheEnd();
            }
        }
    }

    bool AdvanceEncounter()
    {
        if (layout.encounters.Count > currentEncounter) // if the current encounter exists
        {
            float oldlimit = cameraLimit.y;

            // extend camera limit, and thus the current encounter place
            float newlimit = oldlimit + layout.encounters[currentEncounter].reigonSize;
            ExtendLimit(newlimit);

            // restrict limit if set up to
            if (layout.encounters[currentEncounter].restrict)
            {
                ShrinkLimit(oldlimit);
            }

            // create new encounter contact
            var contact = Instantiate(encounterObject);
            contact.transform.position = new Vector3(newlimit - roughHalfCamSize, contact.transform.position.y, contact.transform.position.z);
            
            // setup needed references
            var ce = contact.GetComponent<ContactEncounter>();
            ce.agent = playerObject;
            ce.globalDynamic = gameObject;
            ce.encounter = layout.encounters[currentEncounter];

            // add contact the encounter items
            currentEncounterItems.Add(contact);

            // place the contact on the player if the area size is zero
            if (playerObject.transform.position.x > newlimit - roughHalfCamSize)//layout.encounters[currentEncounter].reigonSize == 0)
            {
                contact.transform.position = new Vector3(playerObject.transform.position.x, contact.transform.position.y, contact.transform.position.z);
            }

            currentEncounter = currentEncounter + 1;
            return true;
        }
        else
        {
            return false;
        }
    }

    void ExtendLimit(float x)
    {
        cameraLimit.y = x;

        var s = topCollider.transform.localScale;
        topCollider.transform.localScale = new Vector3(x, s.y, s.z);
        s = bottomCollider.transform.localScale;
        bottomCollider.transform.localScale = new Vector3(x, s.y, s.z);
    }
    void ShrinkLimit(float x)
    {
        cameraLimit.x = x;
    }
    public GameObject TheEnd()
    {
        var obj = Instantiate(sceneObject);
        obj.transform.position = new Vector3(cameraLimit.y, obj.transform.position.y, obj.transform.position.z);
        obj.GetComponent<TheEndContact>().scene = menu;
        obj.GetComponent<TheEndContact>().agent = playerObject;

        currentEncounterItems.Add(obj);

        return obj;
    }
}

[System.Serializable]
public class LevelLayout
{
    public List<LevelEncounter> encounters;
}

[System.Serializable]
public class LevelEncounter
{
    public float reigonSize = 15f; // how far to extend limit
    public bool restrict = true; // shrink limit to current?
    public List<EnemyEncounter> enemies;
    public List<ObjectEncounter> objects;
}

[System.Serializable]
public class EnemyEncounter
{
    public int count = 1;
    public EncounterSide side;
    public GameObject enemy;
    public float sift = 0.3f;
}

[System.Serializable]
public class ObjectEncounter
{
    public int count = 1;
    public EncounterSide side;
    public GameObject obj;
}

public enum EncounterSide
{ Right, Left, Inside, Anywhere, Outside }
