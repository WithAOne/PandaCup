using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2020 Isabella Dare/WithAOne

public class ContactEncounter : MonoBehaviour
{
    public GameObject globalDynamic;
    public GameObject agent;
    public LevelEncounter encounter;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetInstanceID() == agent.GetInstanceID())
        {
            // spawn in all enemies in the encounter
            foreach (EnemyEncounter enemy in encounter.enemies)
            {
                for (int i = 0; i < enemy.count; i++)
                {
                    Spawn(enemy.enemy, enemy.side);
                }
            }

            Destroy(gameObject);

        }
    }

    void Spawn(GameObject go, EncounterSide side)
    {

        if (side == EncounterSide.Anywhere)
        {
            // choose between it spawning to the left, right or inside
            float chance = Random.Range(0, 3);
            if (chance < 1) { side = EncounterSide.Inside; }
            else if (chance < 2) { side = EncounterSide.Left; }
            else if (chance < 3) { side = EncounterSide.Right; }
        }
        else if (side == EncounterSide.Outside)
        {
            // choose between it spawning to the left or right
            float chance = Random.Range(0, 2);
            if (chance < 1) { side = EncounterSide.Left; }
            else if (chance < 2) { side = EncounterSide.Right; }
        }

        if (side == EncounterSide.Left)
        {
            float x = Random.Range(-Camera.main.orthographicSize * 3, -Camera.main.orthographicSize * 2);
            Spawn(go, x);
        }
        else if (side == EncounterSide.Right)
        {
            float x = Random.Range(Camera.main.orthographicSize * 3, Camera.main.orthographicSize * 2);
            Spawn(go, x);
        }
        else if (side == EncounterSide.Inside)
        {
            float x = Random.Range(-Camera.main.orthographicSize * 1.8f, Camera.main.orthographicSize * 1.8f);
            Spawn(go, x);
        }

    }
    void Spawn(GameObject go, float x)
    {
        var spawned = Instantiate(go);
        spawned.transform.position = new Vector3(Camera.main.transform.position.x + x, Random.Range(-11.5f, 1f), spawned.transform.position.z);

        if (spawned.GetComponent<AIContainer>() != null)
        {
            spawned.GetComponent<AIContainer>().target = agent;
        }

        // add spawned item into the list of current encounter items
        globalDynamic.GetComponent<GlobalDynamic>().currentEncounterItems.Add(spawned);
    }
}
