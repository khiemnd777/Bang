using System.Linq;
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
    public bool isDeath;
    public bool isEnemy;
    public bool isTurn;
    public List<Skill> skills = new List<Skill>();
    public List<Skill> learnedSkills = new List<Skill>();
    public List<Tactical> tactics = new List<Tactical>();
    public List<Ability> abilities = new List<Ability>();

    public delegate void OnAbilityHandled(Character character);
    public OnAbilityHandled onAbilityHandledCallback;

    public void AddSkill(Skill skill)
    {
        skills.Add(skill);
    }

    public Skill LearnSkill(Skill skill)
    {
        var learnedSkill = Instantiate<Skill>(skill, Vector3.zero, Quaternion.identity);
        learnedSkill.transform.SetParent(transform);
        learnedSkill.character = this;
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

    public void AddAbility(Ability ability)
    {
        abilities.Add(ability);
    }

    public void ClearAllAbilities()
    {
        abilities.Clear();
    }

    public void HandleAbilities()
    {
        StartCoroutine(OnAbilityHandling(this));
    }

    IEnumerator OnAbilityHandling(Character owner)
    {
        var validAbilities = owner.abilities
            .Where(x =>
                x.tactics.Any(x1 => x1.Define()));
        
        foreach (var ability in validAbilities.Where(x => x.tactics.Any(x1 => !x1.isDefault)))
        {
            var tactic = ability.tactics.FirstOrDefault();
            yield return StartCoroutine(ability.Use(tactic));
            ability.StopCoroutine("Use");
            break;
        }

        foreach (var ability in validAbilities.Where(x => x.tactics.Any(x1 => x1.isDefault)))
        {
            var tactic = ability.tactics.FirstOrDefault();
            yield return StartCoroutine(ability.Use(tactic));
            ability.StopCoroutine("Use");
            break;
        }

        owner.isTurn = false;

        if (owner.onAbilityHandledCallback != null)
            owner.onAbilityHandledCallback.Invoke(owner);

        validAbilities = null;

        owner.StopCoroutine("OnAbilityHandling");
        owner = null;
    }
}