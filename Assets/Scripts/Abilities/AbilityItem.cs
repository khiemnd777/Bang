using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityItem : MonoBehaviour
{
    public Ability ability;

    public void HandleTitle()
    {
        var title = GetComponentInChildren<Text>();
        if (title.IsNull())
            return;
        title.text = ability.name +
            (!string.IsNullOrEmpty(ability.description)
                && !string.IsNullOrEmpty(ability.name) ? "\n" : "") +
                (!string.IsNullOrEmpty(ability.description) ? ability.description : "");
    }

    public void OnOpenTacticListButtonClick()
    {

    }

    void OnDrawGizmos()
    {
        if (ability.IsNull())
            return;
        HandleTitle();
    }
}