﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    new public string name = "New skill";
    public string description = "New description of ability";
    public int displayOrder;
    public Tactical defaultTacticPrefab;
    public Character character;
    public bool isUsing;
    public List<Tactical> tactics = new List<Tactical>();

    public virtual IEnumerator Use(Tactical tactic)
    {
        yield return null;
    }

    public void AddTactic(Tactical tactic)
    {
        tactic.ability = this;
        tactic.transform.SetParent(transform);
        tactics.Add(tactic);
    }

    public FieldSlot[] GetFieldSlots()
    {
        var ownFieldSlots = !character.isEnemy
            ? BattleFieldManager.instance.playerFieldSlots
            : BattleFieldManager.instance.opponentFieldSlots;
        return ownFieldSlots;
    }

    public FieldSlot[] GetOpponentFieldSlots()
    {
        var opponentFieldSlots = !character.isEnemy
            ? BattleFieldManager.instance.opponentFieldSlots
            : BattleFieldManager.instance.playerFieldSlots;
        return opponentFieldSlots;
    }

    public FieldSlot GetOwnFieldSlot()
    {
        var fieldSlots = GetFieldSlots();
        var single = fieldSlots.FirstOrDefault(x => x.character == character);
        fieldSlots = null;
        return single;
    }
}