using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DefaultTactic : Tactical
{
    public override bool Define()
    {
        base.Define();
        priorityPositions = FindPriorityPositions();
        if(priorityPositions.Length <= 0)
            return false;
        return true;
    }

    int[] FindPriorityPositions()
    {
        var ownFieldSlots = ability.GetFieldSlots();
        var opponentFieldSlots = ability.GetOpponentFieldSlots();

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
            if (ownFieldSlots[i].character == ability.character)
                break;
        }

        var index = 3 * currentOwnRow + 2;
        var priorityIndexes = new List<int>();
        for (var i = 0; i < 9; i++)
        {
            var opponentCharacter = opponentFieldSlots[index].character;
            if (!opponentCharacter.IsNull() && !opponentCharacter.isDeath)
            {
                priorityIndexes.Add(index);
            }
            if (index % 3 == 0)
            {
                ++currentOwnRow;
                if (currentOwnRow > 2)
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