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
    public List<Skill> learnedSkills = new List<Skill>();

    public void UseSkill()
    {
        foreach (var skill in skills)
        {
            skill.Use();
        }
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

    public void AddSkill(Skill skill)
    {
        skill.owner = this;
        skills.Add(skill);
    }
}