using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DragDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image icon;
    public Color hoverColor;
    public bool isDrag;
    public bool isEndDrag;
    
    public delegate void OnDragged(GameObject item, bool isAlternative);
    public OnDragged onDragged;

    DragDropHandler[] items;
    Color originalColor;
    Vector3 startPosition;
    Vector3 originalIconScale;
    Vector3 lastDraggableIconPosition;
    Image draggableIcon;
    Canvas canvas;
    
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

            StartCoroutine(OnBeginDragging());
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggableIcon != null)
        {
            draggableIcon.transform.position = Input.mousePosition;
            foreach (var item in items)
            {
                if (!RectTransformUtility.RectangleContainsScreenPoint(item.GetComponent<RectTransform>(), Input.mousePosition))
                {
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

            DragDropHandler matchItem = null;
            var isAlternative = false;
            foreach (var item in items)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(item.GetComponent<RectTransform>(), Input.mousePosition))
                {
                    if (item != this)
                    {
                        if (item.icon.enabled)
                        {
                            var itemIconSprite = item.icon.sprite;
                            item.icon.sprite = icon.sprite;

                            icon.sprite = itemIconSprite;
                            icon.enabled = true;
                            isAlternative = true;
                        }
                        else
                        {
                            item.icon.sprite = icon.sprite;
                            icon.enabled = false;
                        }
                        item.icon.enabled = true;
                        matchItem = item;
                    }
                }
                item.transform.localScale = Vector3.one;
                item.GetComponent<Image>().color = originalColor;
            }
            if (matchItem != null)
            {
                Destroy(draggableIcon.gameObject);
                StartCoroutine(OnSlotMatch(matchItem, icon));
                if(matchItem.onDragged != null){
                    matchItem.onDragged.Invoke(this.gameObject, isAlternative);
                }
            }
            else
            {
                StartCoroutine(OnSlotMiss());
            }
        }
    }

    IEnumerator OnBeginDragging()
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

    IEnumerator OnSlotMiss()
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

    IEnumerator OnSlotMatch(DragDropHandler matchItem, Image currentIcon)
    {
        var fracJourney = 0f;
        while (fracJourney < 1f)
        {
            var distCovered = (Time.time - startEndDragTime) * 5f;
            fracJourney = distCovered / endDragJourneyLength;
            if (currentIcon.enabled)
                currentIcon.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, fracJourney);

            matchItem.icon.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, fracJourney);

            yield return null;
        }
    }
}
