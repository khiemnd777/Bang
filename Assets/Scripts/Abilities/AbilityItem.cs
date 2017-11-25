using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityItem : MonoBehaviour
{
    public Ability ability;
    public RectTransform titleContainer;
    public Transform tacticContainer;
    public TacticItem tacticItemPrefab;

    DragDropHandler dragDropHandler;
    RectTransform rectTransform;
    float minHeight;

    void Start()
    {
        dragDropHandler = GetComponent<DragDropHandler>();
        rectTransform = GetComponent<RectTransform>();
        minHeight = titleContainer.GetHeight() + 10f;
        FitWithTacticContainer();
        
        dragDropHandler.onDragged += OnItemDragged;
    }

    void Update()
    {
        FitWithTacticContainer();
    }

    void OnItemDragged(GameObject item, bool isAlternative){
        AbilityList.instance.SetDisplayOrder();
    }

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

    public void InstantiateTacticItems(){
        var tactics = ability.tactics;
        foreach(var tactic in tactics){
            tactic.displayOrder = GetNextTacticDisplayOrder();
            var tacticItem = Instantiate<TacticItem>(tacticItemPrefab, Vector2.zero, Quaternion.identity, tacticContainer);
            tacticItem.abilitiItem = this;
            tacticItem.tactic = tactic;
            tacticItem.HandleTitle();
            tacticItem = null;
        }
        tactics = null;
    }

    int GetNextTacticDisplayOrder()
    {
        var tacticItems = tacticContainer.GetComponentsInChildren<TacticItem>();
        if(tacticItems.Length == 0){
            return 1;
        }
        var nextDisplayOrder = tacticItems.Max(x => x.tactic.displayOrder) + 1;
        tacticItems = null;
        return nextDisplayOrder;
    }

    public void SetTacticDisplayOrder()
    {
        var tacticItems = tacticContainer.GetComponentsInChildren<TacticItem>();
        for (var i = 0; i < tacticItems.Length; i++)
        {
            var item = tacticItems[i];
            item.tactic.displayOrder = i + 1;
            item = null;
        }
        tacticItems = null;
    }

    public void ClearAllTacticItems(){
        var tacticItems = tacticContainer.GetComponentsInChildren<TacticItem>();
        foreach(var tacticItem in tacticItems){
            DestroyImmediate(tacticItem.gameObject);
        }
    }

    public void ToggleTacticContainer()
    {
        tacticContainer.gameObject.SetActive(!tacticContainer.gameObject.activeSelf);
    }

    void FitWithTacticContainer()
    {
        if(!tacticContainer.gameObject.activeSelf){
            rectTransform.SetHeight(minHeight);
            return;
        }
        var totalTacticContainerHeight = 0f;
        var paddingBottomTacticItem = 10f;
        var tacticItems = tacticContainer.GetComponentsInChildren<TacticItem>();
        var paddingBottom = (tacticItems.Length > 0 ? 1 : -1) * 5f;
        tacticContainer.gameObject.SetActive(tacticItems.Length > 0);
        foreach (var tacticItem in tacticItems)
        {
            var tacticItemRectTransform = tacticItem.GetComponent<RectTransform>();
            var singleHeight = tacticItemRectTransform.GetHeight();
            totalTacticContainerHeight += singleHeight + paddingBottomTacticItem;
            tacticItemRectTransform = null;
        }
        rectTransform.SetHeight(minHeight + totalTacticContainerHeight + paddingBottom);
        tacticItems = null;
    }

    void OnDrawGizmos()
    {
        if (ability.IsNull())
            return;
        HandleTitle();
    }
}