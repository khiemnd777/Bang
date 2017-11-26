using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AvailableTacticItem : MonoBehaviour
{
    public Tactical tactic;
    public TacticItem tacticItemPrefab;

    DropZoneHandler dropZoneHandler;

    Text title;

    void Start()
    {
        dropZoneHandler = GetComponent<DropZoneHandler>();
        dropZoneHandler.onDragInZoneEvent += OnItemDropInZone;
    }

    void OnItemDropInZone(PointerEventData eventData)
    {
        Debug.Log("InZone");
    }

    public void HandleTitle()
    {
        title = GetComponentInChildren<Text>();
        title.text = tactic.description;
    }

    void OnDrawGizmos()
    {
        if (tactic.IsNull())
            return;
        HandleTitle();
    }
}