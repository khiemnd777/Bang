using UnityEngine;
using UnityEngine.UI;

public class FieldSlot: MonoBehaviour
{
    public Image icon;

    Character character;

    public void AddField(Character character){
        this.character = character;
        icon.sprite = character.icon;
        icon.enabled = true;
    }

    public void ClearSlot(){
        this.character = null;
    }
}