using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefaultSkill : Skill
{
    public override int[] Use()
    {
        base.Use();
        
        var ownFieldSlots = !owner.isEnemy
            ? BattleFieldManager.instance.playerFieldSlots
            : BattleFieldManager.instance.opponentFieldSlots;

        var opponentFieldSlots = !owner.isEnemy
            ? BattleFieldManager.instance.opponentFieldSlots
            : BattleFieldManager.instance.playerFieldSlots;

        var currentOwnCol = 0;
        var currentOwnRow = 0;

        for (var i = 0; i < ownFieldSlots.Length; i++)
        {
            if (i > 0 && i % 3 == 0)
            {
                ++currentOwnRow;
                currentOwnCol = 0;
            }
            else
            {
                ++currentOwnCol;
            }
            if (ownFieldSlots[i].character == owner)
                break;
        }

        var index = 3 * currentOwnRow + 2;
        var priorityIndexes = new List<int>();
        for (var i = 0; i < 9; i++)
        {
            if (!opponentFieldSlots[index].character.IsNull())
            {
                priorityIndexes.Add(index);
            }
            if(index % 3 == 0){
                ++currentOwnRow;
                if(currentOwnRow > 2)
                    currentOwnRow = 0;
                index = 3 * currentOwnRow + 2;
                continue;
            }
            --index;
        }

        ownFieldSlots = null;
        opponentFieldSlots = null;

        return priorityIndexes.ToArray();
    }
}