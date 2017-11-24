using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TacticItem : MonoBehaviour
{
    public Tactical tactic;

    Text title;

    void OnDrawGizmos()
    {
        if(tactic.IsNull())
            return;
        title = GetComponentInChildren<Text>();
        title.text = tactic.description;
    }
}