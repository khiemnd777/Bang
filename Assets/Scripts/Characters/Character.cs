using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    new public string name = "New Character";
    public float health;
    public float dexterity;
    public Sprite icon;
    public bool isEnemy;
    public List<Skill> skills = new List<Skill>();
    public List<Skill> learnedSkills = new List<Skill>();

    public void UseSkill()
    {
        foreach (var skill in skills)
        {
            skill.Use();
        }
    }

    public void AddSkill(Skill skill)
    {
        skills.Add(skill);
    }

    public void LearnSkill(Skill skill)
    {
        var learnedSkill = Instantiate<Skill>(skill, Vector3.zero, Quaternion.identity);
        learnedSkill.owner = this;
        learnedSkills.Add(learnedSkill);
    }

    public void ClearAllLearnedSkills(){
        learnedSkills.Clear();
    }
}