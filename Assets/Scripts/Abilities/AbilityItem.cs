﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityItem : MonoBehaviour
{
    public Ability ability;
    public RectTransform titleContainer;
    public Transform tacticContainer;

    RectTransform rectTransform;
    float minHeight;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        minHeight = titleContainer.GetHeight() + 10f;
    }

    void Update()
    {
        FitWithTacticContainer();
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