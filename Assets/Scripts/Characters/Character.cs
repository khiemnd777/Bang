using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    new public string name = "New Character";
    public float health;
    [Header("Stats")]
    public float dexterity;
    public Sprite icon;
    public bool isEnemy;
    public bool isTurn;
    public List<Skill> skills = new List<Skill>();
    public List<Skill> learnedSkills = new List<Skill>();
    public List<Tactical> tactics = new List<Tactical>();

    public delegate void OnTacticHandled(Character character);
    public OnTacticHandled onTacticHandledCallback;

    public void AddSkill(Skill skill)
    {
        skills.Add(skill);
    }

    public Skill LearnSkill(Skill skill)
    {
        var learnedSkill = Instantiate<Skill>(skill, Vector3.zero, Quaternion.identity);
        learnedSkill.transform.SetParent(transform);
        learnedSkill.owner = this;
        learnedSkills.Add(learnedSkill);
        return learnedSkill;
    }

    public void ClearAllLearnedSkills()
    {
        learnedSkills.Clear();
    }

    public void AddTactic(Tactical tactic)
    {
        tactics.Add(tactic);
    }

    public void ClearAllTactics()
    {
        tactics.Clear();
    }

    public void HandleTactics()
    {
        StartCoroutine(OnTacticHandling(this));
    }

    IEnumerator OnTacticHandling(Character owner)
    {
        foreach (var tactic in owner.tactics)
        {
            yield return StartCoroutine(tactic.Use());
            tactic.StopCoroutine("Use");
        }

        owner.isTurn = false;

        if (owner.onTacticHandledCallback != null)
            owner.onTacticHandledCallback.Invoke(owner);

        owner.StopCoroutine("OnTacticHandling");
        owner = null;
    }
}