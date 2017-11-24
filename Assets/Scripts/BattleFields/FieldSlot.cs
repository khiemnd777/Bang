using UnityEngine;
using UnityEngine.UI;

public class FieldSlot : MonoBehaviour
{
    public Image icon;
    public Image priorityIndex;
    [System.NonSerialized]
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

    public void OnShowSkillPanelButtonClick(){
        AbilityList.instance.Clear();
        foreach(var skill in character.learnedSkills){
            AbilityList.instance.AddItem(skill);
        }
	}

    void OnUpdateSlot(GameObject item, bool isAlternative)
    {
        // inactive priorityIndex
        foreach(var slot in BattleFieldManager.instance.playerFieldSlots){
            slot.priorityIndex.gameObject.SetActive(false);
        }
        foreach(var slot in BattleFieldManager.instance.opponentFieldSlots){
            slot.priorityIndex.gameObject.SetActive(false);
        }
        
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