using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public Character owner;
    public bool isUsing;
    public List<Tactical> tactics = new List<Tactical>();

    public void AddTactic(Tactical tactic){
        tactic.ability = this;
        tactics.Add(tactic);
    }

    protected FieldSlot[] GetFieldSlots()
    {
        var ownFieldSlots = !owner.isEnemy
            ? BattleFieldManager.instance.playerFieldSlots
            : BattleFieldManager.instance.opponentFieldSlots;
        return ownFieldSlots;
    }

    protected FieldSlot[] GetOpponentFieldSlots()
    {
        var opponentFieldSlots = !owner.isEnemy
            ? BattleFieldManager.instance.opponentFieldSlots
            : BattleFieldManager.instance.playerFieldSlots;
        return opponentFieldSlots;
    }

    protected FieldSlot GetOwnFieldSlot(){
        var fieldSlots = GetFieldSlots();
        var single = fieldSlots.FirstOrDefault(x => x.character == owner);
        fieldSlots = null;
        return single;
    }

    public virtual IEnumerator Use()
    {
        yield return null;
    }

    public virtual int[] FindPriorityPositions()
    {
        return null;
    }
}