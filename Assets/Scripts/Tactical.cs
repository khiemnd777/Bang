using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tactical : MonoBehaviour
{
    public Ability ability;
    public Tactical owner;
    public List<Tactical> tactics = new List<Tactical>();

    public virtual IEnumerator Use()
    {
        yield return null;
    }

    public virtual int[] FindPriorityPositions()
    {
        return null;
    }

    public void AddTactic(Tactical tactic){
        tactic.owner = this;
        tactic.ability = ability;
        tactics.Add(tactic);
    }
}