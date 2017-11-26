using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DragDropHandler))]
public class DropZoneHandler : MonoBehaviour
{
    public delegate void OnDragInZoneEvent(PointerEventData eventData);
    public OnDragInZoneEvent onDragInZoneEvent;

    DragDropHandler handler;

    void Start()
    {
        handler = GetComponent<DragDropHandler>();
        handler.onDragEvent += OnDrag;
    }

    void OnDrag(PointerEventData eventData)
    {
        var position = eventData.position;

        // interactable cases
        var interactableZones = FindObjectsOfType<InteractableZone>();
        if (interactableZones.Length == 0)
            return;
        foreach (var interactableZone in interactableZones)
        {
            var rectInteractableZone = interactableZone.GetComponent<RectTransform>();
            if (rectInteractableZone.IsNull())
                continue;
            if (RectTransformUtility.RectangleContainsScreenPoint(rectInteractableZone, position))
            {
                var droppableZones = rectInteractableZone.GetComponentsInChildren<DroppableZone>();
                foreach (var zone in droppableZones)
                {
                    var rectZone = zone.GetComponent<RectTransform>();
                    if (RectTransformUtility.RectangleContainsScreenPoint(zone.GetComponent<RectTransform>(), position))
                    {
                        if (onDragInZoneEvent != null)
                        {
                            onDragInZoneEvent.Invoke(eventData);
                        }
                    }
                    rectZone = null;
                }
                droppableZones = null;
            }
            rectInteractableZone = null;
        }
    }
}
