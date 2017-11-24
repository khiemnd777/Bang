using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TacticalUI : MonoBehaviour
{
    public Tactical tactic;

    Text title;

    void OnDrawGizmos()
    {
        if(tactic.IsNull())
        title = GetComponentInChildren<Text>();
        title.text = tactic.description;
    }
}