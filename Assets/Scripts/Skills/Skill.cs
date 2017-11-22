using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Ability
{
    new public string name = "New Skill";
    public Sprite icon;
    public Color markColor;
    public Color selectColor;
    
    public override IEnumerator Use() 
    {
        Debug.Log(owner.name + " is using skill " + name);
        yield return base.Use();
    }
}