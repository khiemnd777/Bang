using UnityEngine;

public class DefaultSkill : Skill
{
    public override void Use()
    {
        base.Use();
        var playerFieldSlots = BattleFieldManager.instance.playerFieldSlots; 
        var opponentFieldSlots = BattleFieldManager.instance.opponentFieldSlots;

    }
}