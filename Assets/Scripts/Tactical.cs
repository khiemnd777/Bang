using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tactical : MonoBehaviour
{
    public Character owner;
    public bool isUsing;

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