using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatContainer : MonoBehaviour
{
    public float Health = 10;
    public bool destroyOnDead = true;
    public string animationOnDead = "";

    private bool dead = false;

    public void DamageTaken(float damage)
    {
        if (Health <= 0)
        {
            if (destroyOnDead)
            {
                Destroy(gameObject);
            }
            if (!destroyOnDead && !dead)
            {
                GetComponent<CharacterControl>().visual.GetComponent<Animator>().Play(animationOnDead);

                // disable controls and velocity
                if(GetComponent<CharacterControl>())
                { GetComponent<CharacterControl>().enabled = false; }
                if(GetComponent<Rigidbody2D>())
                { Destroy(GetComponent<Rigidbody2D>()); }

                // end level
                var f = GameObject.Find("GlobalDynamic").GetComponent<GlobalDynamic>().TheEnd();
                f.GetComponent<TheEndContact>().ending = true;
            }

            dead = true;
        }
    }
}
