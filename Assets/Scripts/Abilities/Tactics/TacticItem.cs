using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TacticItem : MonoBehaviour
{
    public Tactical tactic;

    Text title;

    public void HandleTitle(){
        title = GetComponentInChildren<Text>();
        title.text = tactic.description;
    }

    void OnDrawGizmos()
    {
        if(tactic.IsNull())
            return;
        HandleTitle();
    }
}