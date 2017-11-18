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

    public void OnUseButtonClick(){
		// if(character == null)
		// 	return;

        // var lastLearnedSkill = character.learnedSkills[character.learnedSkills.Count - 1];
        // lastLearnedSkill.Use();

        // // inactive priorityIndex
        // foreach(var slot in BattleFieldManager.instance.playerFieldSlots){
        //     slot.priorityIndex.gameObject.SetActive(false);
        // }
        // foreach(var slot in BattleFieldManager.instance.opponentFieldSlots){
        //     slot.priorityIndex.gameObject.SetActive(false);
        // }

        // var fieldSlots = character.isEnemy ? BattleFieldManager.instance.playerFieldSlots : BattleFieldManager.instance.opponentFieldSlots;
        // for(var i = 0; i < fieldSlots.Length; i++){
        //     for(var ii = 0; ii < lastLearnedSkill.priorityPositions.Length; ii++){
        //         if(i != lastLearnedSkill.priorityPositions[ii])
        //             continue;
        //         var fieldSlot = fieldSlots[i];
        //         fieldSlot.priorityIndex.gameObject.SetActive(true);
        //         fieldSlot.priorityIndex.GetComponentInChildren<Text>().text = (ii + 1).ToString();
        //         fieldSlot = null;
        //     }
        // }
        // fieldSlots = null;
        // lastLearnedSkill = null;
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