using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DefaultSkill : Skill
{
    public override IEnumerator Use()
    {
        base.Use();

        var positions = FindPriorityPositions();
        var opponentFieldSlots = GetOpponentFieldSlots();
        var opponentFieldSlot = opponentFieldSlots[positions[0]];
        var opponentImage = opponentFieldSlot.GetComponent<Image>();
        var ownFieldSlot = GetOwnFieldSlot();
        var ownImage = ownFieldSlot.GetComponent<Image>();

        opponentImage.color = markColor;
        ownImage.color = selectColor;
        
        yield return new WaitForSeconds(.125f);

        opponentImage.color = Color.white;
        ownImage.color = Color.white;
        
        opponentImage = null;
        ownImage = null;
        positions = null;
        opponentFieldSlots = null;
        opponentFieldSlot = null;
    }

    public override int[] FindPriorityPositions()
    {
        var ownFieldSlots = GetFieldSlots();
        var opponentFieldSlots = GetOpponentFieldSlots();

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