using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackAI : MonoBehaviour
{
    AIContainer aicon;

    public List<CombatSkill> skills;
    public float skillCooldown = 0;

    void Start()
    {
        aicon = GetComponent<AIContainer>();
    }
    void Update()
    {
        var target = aicon.target;

        if (skillCooldown == 0)
        {
            foreach (CombatSkill skill in skills)
            {
                // only use the skill if within range
                if (Vector3.Distance(transform.position, target.transform.position) <= skill.range)
                {

                    if (skill.effectType == SkillType.Aura)
                    {
                        // knockback
                        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
                        if (rb != null)
                        {
                            // new position
                            var npos = transform.position;

                            // direction
                            var dir = (rb.transform.position - npos).normalized * skill.knockback * 1000;
                            rb.AddForceAtPosition(new Vector2(dir.x, dir.y), transform.position);
                        }

                        // damage
                        StatContainer sc = target.GetComponent<StatContainer>();
                        if (sc != null)
                        {
                            sc.Health -= skill.damage;
                            target.SendMessage("DamageTaken", skill.damage); // inform object it's been damaged
                        }
                    }

                    skillCooldown = skill.cooldown;

                }
            }
        }

        skillCooldown = Mathf.Max(0, skillCooldown - Time.deltaTime);
    }
}
