using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldSlot : MonoBehaviour
{
    public Image icon;
    public Image priorityIndex;
    public Character character;

    DragDropHandler dragDropHandler;
    Canvas canvas;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        dragDropHandler = GetComponent<DragDropHandler>();
        dragDropHandler.onDragged += OnUpdateSlot;
    }

    public void AddField(Character character)
    {
        this.character = character;
        icon.sprite = character.icon;
        icon.enabled = true;
    }

    public bool CanAdd(){
        return character.IsNull();
    }

    public void ClearSlot()
    {
        this.character = null;
        icon.enabled = false;
    }

    public void OnShowSkillPanelButtonClick()
    {
        if(character.IsNull())
            return;
        var abilityList = AbilityList.instance;
        abilityList.Clear();
        foreach (var skill in character.learnedSkills)
        {
            if(skill.isDefault)
                continue;
            abilityList.AddItem(skill);
        }
        abilityList.OpenPanel(transform.position);
        abilityList = null;
    }

    void OnUpdateSlot(GameObject item, int index, bool isAlternative)
    {
        // inactive priorityIndex
        foreach (var slot in BattleFieldManager.instance.playerFieldSlots)
        {
            slot.priorityIndex.gameObject.SetActive(false);
        }
        foreach (var slot in BattleFieldManager.instance.opponentFieldSlots)
        {
            slot.priorityIndex.gameObject.SetActive(false);
        }

        var oldFieldSlot = item.GetComponent<FieldSlot>();
        if (isAlternative)
        {
            var currentCharacter = character;
            AddField(oldFieldSlot.character);
            oldFieldSlot.AddField(currentCharacter);
            oldFieldSlot.character.slot = character.slot;
            character.slot = index;
        }
        else
        {
            AddField(oldFieldSlot.character);
            oldFieldSlot.ClearSlot();
            character.slot = index;
        }
    }
}