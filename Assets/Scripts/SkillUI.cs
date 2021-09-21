using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2020 Isabella Dare/WithAOne

public class SkillUI : MonoBehaviour
{
    public GameObject owner; // the object from which the skills are taken
    public bool isControl = false;
    public int skill;

    private UnityEngine.UI.Image image;
    private CharacterControl cc;
    private UnityEngine.UI.Button button;

    void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
        cc = owner.GetComponent<CharacterControl>();
        if (!isControl)
        {
            button = GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(OnClick);
        }
    }
    void Update()
    {
        if (isControl)
        {
            // dim if not usable
            if (cc.skills[skill].cooldownTimer > 0)
            { image.color = new Color(0.5f, 0.5f, 0.5f); }
            else
            { image.color = new Color(1f, 1f, 1f); }
        }
        else
        {
            // dim when cooling down
            // higher limit 0.8, lower limit 0.2
            CombatSkill cskill = cc.skills[skill];
            float dim = 1 - Mathf.Max(0, (cskill.cooldownTimer / cskill.cooldown) - 0.2f);
            image.color = new Color(dim, dim, dim);
        }
    }

    void OnClick()
    {
        if (cc.skills[skill].cooldownTimer == 0)
        { cc.UseSkill(cc.skills[skill]); }
    }
}
