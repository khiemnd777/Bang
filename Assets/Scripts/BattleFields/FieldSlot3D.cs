using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSlot3D : MonoBehaviour
{
	public Character character;

	public void AddField(Character character)
    {
        this.character = character;
    }

	public void ClearSlot()
    {
        this.character = null;
    }
}
