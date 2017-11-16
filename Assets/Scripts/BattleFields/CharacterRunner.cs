using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CharacterRunner : MonoBehaviour
{
	public Image icon;

	void Awake(){
		icon = GetComponent<Image>();
	}
}