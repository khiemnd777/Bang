using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public List<Skill> skills = new List<Skill>();

	public void AddSkill(Skill skill){
		skill.owner = this;
		skills.Add(skill);
	}
}