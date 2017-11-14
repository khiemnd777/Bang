using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/New Character")]
public class Character : ScriptableObject
{
    new public string name = "New Character";
    public float health;
    public Sprite icon;
    public bool isEnemy;
    public List<Skill> skills = new List<Skill>();

    public void UseSkill()
    {
        foreach (var skill in skills)
        {
            skill.Use();
        }
    }

    public void AddSkill(Skill skill)
    {
        skill.owner = this;
        skills.Add(skill);
    }
}