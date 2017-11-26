using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TacticItem : MonoBehaviour
{
    public Tactical tactic;
    public AbilityItem abilityItem;

    DragDropHandler dragDropHandler;
    Text title;

    void Start(){
        dragDropHandler = GetComponent<DragDropHandler>();
        dragDropHandler.onDragged += OnItemDragged;
    }

    void OnItemDragged(GameObject item, bool isAlternative)
    {
        abilityItem.SetTacticDisplayOrder();
    }

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