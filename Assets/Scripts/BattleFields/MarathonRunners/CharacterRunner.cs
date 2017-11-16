using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterRunner : MonoBehaviour
{
    public Image icon;
    public Character character;
	public float deltaSpeed = 5.625f;
	
	MarathonRunner marathonRunner;
	RectTransform runnerRectTranform;
	RectTransform rectTransform;
	float startTime;
    float journeyLength;

	void Start(){
		rectTransform = GetComponent<RectTransform>();
		marathonRunner = GetComponentInParent<MarathonRunner>();
		runnerRectTranform = marathonRunner.GetComponent<RectTransform>();
		startTime = Time.time;
		journeyLength = runnerRectTranform.GetWidth();
	}

    void Update()
    {
		var distCovered = (Time.time - startTime) * character.dexterity * deltaSpeed;
		var fracJourney = distCovered / journeyLength;
		rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(0f, runnerRectTranform.GetWidth(), fracJourney), 0f);
		if(fracJourney >= 1f){
			startTime = Time.time;
			rectTransform.anchoredPosition = Vector2.zero;
		}
    }
}