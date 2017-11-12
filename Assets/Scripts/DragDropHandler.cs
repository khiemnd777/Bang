﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DragDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image icon;
    public Color hoverColor;

    DragDropHandler[] items;
    Color originalColor;
    Vector3 startPosition;
    Vector3 originalIconScale;
    Vector3 lastDraggableIconPosition;
    Image draggableIcon;
    Canvas canvas;
    bool isDrag;
    bool isEndDrag;
    float startDragTime;
    float startEndDragTime;
    float dragJourneyLength;
    float endDragJourneyLength;

    void Start()
    {
        items = transform.parent.GetComponentsInChildren<DragDropHandler>();
        originalColor = GetComponent<Image>().color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvas = GetComponentInParent<Canvas>();
        startPosition = transform.position;

        if (icon.enabled)
        {
            startDragTime = Time.time;
            dragJourneyLength = Vector3.Distance(Vector3.one, Vector3.zero);

            if (draggableIcon != null)
                Destroy(draggableIcon.gameObject);

            draggableIcon = Instantiate<Image>(icon, Input.mousePosition, Quaternion.identity);
            draggableIcon.sprite = icon.sprite;
            draggableIcon.transform.localScale = icon.transform.lossyScale;
            draggableIcon.transform.SetParent(canvas.transform, false);

            isDrag = true;
            isEndDrag = false;

            StartCoroutine(OnDragging());
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggableIcon != null)
        {
            draggableIcon.transform.position = Input.mousePosition;
            foreach (var item in items)
            {
                if (!RectTransformUtility.RectangleContainsScreenPoint(item.GetComponent<RectTransform>(), Input.mousePosition)){
                    item.transform.localScale = Vector3.one;
                    item.GetComponent<Image>().color = originalColor;
                    continue;
                }
                item.transform.localScale = Vector3.one * 1.1f;
                item.GetComponent<Image>().color = hoverColor;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        isEndDrag = true;
        dragJourneyLength = 0f;

        if (draggableIcon != null)
        {
            startEndDragTime = Time.time;
            endDragJourneyLength = Vector3.Distance(Vector3.one * 1.5f, Vector3.one);
            lastDraggableIconPosition = draggableIcon.transform.position;

            StartCoroutine(OnEndDragging());
        }
        foreach (var item in items)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(item.GetComponent<RectTransform>(), Input.mousePosition)){
                
            }
            item.transform.localScale = Vector3.one;
            item.GetComponent<Image>().color = originalColor;
        }
    }

    IEnumerator OnDragging()
    {
        var fracJourney = 0f;
        while (fracJourney < 1f)
        {
            var distCovered = (Time.time - startDragTime) * 8f;
            fracJourney = distCovered / dragJourneyLength;
            icon.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, fracJourney);
            yield return null;
        }
    }

    IEnumerator OnEndDragging()
    {
        var fracJourney = 0f;
        while (draggableIcon != null && fracJourney < 1f)
        {
            var distCovered = (Time.time - startEndDragTime) * 5f;
            fracJourney = distCovered / endDragJourneyLength;
            icon.transform.localScale = Vector3.Lerp(Vector3.one * 1.5f, Vector3.one, fracJourney);

            draggableIcon.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, fracJourney);
            draggableIcon.transform.position = Vector3.Lerp(lastDraggableIconPosition, startPosition, fracJourney);

            yield return null;
        }
        if (draggableIcon != null)
            Destroy(draggableIcon.gameObject);
    }
}
