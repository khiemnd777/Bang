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
        dropZoneHandler.onDropInZoneEvent += OnItemDroppedInZone;
    }

    void OnItemDropInZone(GameObject draggableZone, PointerEventData eventData)
    {

    }

    void OnItemDroppedInZone(GameObject droppableZone, PointerEventData eventData)
    {
        droppableZone.SetActive(true);
        var instanceTacticItem = Instantiate(tacticItemPrefab, Vector2.zero, Quaternion.identity, droppableZone.transform);
        instanceTacticItem.tactic = tactic;
        var abilityItem = droppableZone.GetComponent<AbilityItem>();
        if(abilityItem != null){
            abilityItem.ShowTacticContainer();
            instanceTacticItem.abilityItem = abilityItem;
        }
        instanceTacticItem = null;
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