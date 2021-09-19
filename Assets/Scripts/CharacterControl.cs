using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    Rigidbody2D rb;
    Animator ani;

    // the object that is the character's visual puppet
    public GameObject visual;

    public List<CombatSkill> skills = new List<CombatSkill>();

    Vector2 movement = new Vector2();
    [System.NonSerialized] public bool moving = false;
    [System.NonSerialized] public bool attacking = false;
    private CombatSkill attackSkill;
    private bool hasAttacked = false; // if damage has been dealt with the current attack
    public float face = 1; // for stuff like attacks

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = visual.GetComponent<Animator>();
    }
    void Update()
    {
        // check attack animation
        var fr = ani.GetCurrentAnimatorStateInfo(0);
        if (attacking && !fr.IsName(attackSkill.animation))
        {
            attacking = false;
            hasAttacked = false;
        }

        // 
        if (!hasAttacked && attacking && fr.normalizedTime >= (fr.length / 5) * 4)
        {
            // create object that damages enemies
            CreateEffect(attackSkill);
            hasAttacked = true;
        }


        moving = false;

        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        if (movement.magnitude > 0)
        {
            // move character
            var m = movement * 0.1f;
            rb.velocity += m;

            // orient properly
            var s = visual.transform.localScale; // visual's scale
            if (movement.x > 0) // facing right
            {
                s.x = Mathf.Abs(s.x); // reversed absolute of the x to flip the visual
                visual.transform.localScale = s;
                face = 1;
            }
            else if (movement.x < 0) // facing left
            {
                s.x = -Mathf.Abs(s.x); // absolute of the x to straighten the visual
                visual.transform.localScale = s;
                face = -1;
            }

            moving = true;
        }

        // skills
        foreach(CombatSkill skill in skills)
        {
            bool use = false;

            // check if the skill is being used by the input type
            if (skill.inputType == InputType.ButtonDown && Input.GetButtonDown(skill.input))
            { use = true; }
            else if (skill.inputType == InputType.ButtonUp && Input.GetButtonUp(skill.input))
            { use = true; }
            else if (skill.inputType == InputType.ButtonHeld && Input.GetButton(skill.input))
            { use = true; }

            // use skill if cooled down
            if (use && skill.cooldownTimer == 0)
            {
                UseSkill(skill);
            }

            // update cooldown
            skill.cooldownTimer = Mathf.Max(0, skill.cooldownTimer - Time.deltaTime);
        }

        // animate for walking
        ani.SetBool("Walking", moving);
    }

    void CreateEffect(CombatSkill skill)
    {
        // make sure the attack makes the effected push away from the player rather than the effect contact
        var contact = Instantiate(skill.effect, transform.position + new Vector3(2 * face, 0, 0), new Quaternion());
        contact.GetComponent<EffectContact>().contactFrom = gameObject;
        contact.GetComponent<EffectContact>().owner = gameObject;
    }
    public void UseSkill(CombatSkill skill)
    {
        // play attack animation
        ani.Play(skill.animation);
        attacking = true;
        attackSkill = skill;

        // reset cooldown
        skill.cooldownTimer = skill.cooldown;
    }
}

public enum InputType
{ ButtonDown, ButtonUp, ButtonHeld, Axis }
public enum SkillType
{ ContactEffect, Aura }

[System.Serializable]
public class CombatSkill
{
    public string Name = "unnamed";

    [Header("Settings")]
    public string input;
    public InputType inputType;
    public bool canMove = true;
    public string animation;

    [Header("Stats")]
    public float cooldown = 0.5f;
    [System.NonSerialized] public float cooldownTimer = 0;
    public float damage = 1;
    public float knockback = 1;
    public float range = 3;

    public SkillType effectType;
    public GameObject effect;
}
