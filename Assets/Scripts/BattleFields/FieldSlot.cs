using UnityEngine;
using UnityEngine.UI;

public class FieldSlot : MonoBehaviour
{
    public Image icon;
    public Character character;

    DragDropHandler dragDropHandler;

    void Start()
    {
        dragDropHandler = GetComponent<DragDropHandler>();
        dragDropHandler.onDragged += OnUpdateSlot;
    }

    public void AddField(Character character)
    {
        this.character = character;
        icon.sprite = character.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        this.character = null;
        icon.enabled = false;
    }

    void OnUpdateSlot(GameObject item, bool isAlternative)
    {
        var oldFieldSlot = item.GetComponent<FieldSlot>();
        if (isAlternative)
        {
            var currentCharacter = character;
            AddField(oldFieldSlot.character);
            oldFieldSlot.AddField(currentCharacter);
        }
        else
        {
            AddField(oldFieldSlot.character);
            oldFieldSlot.ClearSlot();
        }
    }
}