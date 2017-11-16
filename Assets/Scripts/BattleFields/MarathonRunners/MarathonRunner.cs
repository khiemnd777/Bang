using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarathonRunner : MonoBehaviour
{
    public Transform characterArea;
    public Transform enemyArea;

    public CharacterRunner characterRunnerPrefab;

    List<CharacterRunner> characterRunners = new List<CharacterRunner>();
    List<CharacterRunner> enemyRunners = new List<CharacterRunner>();

    public void AddToCharacterArea(Character character)
    {
        var runner = CreateRunner(character, characterArea);
        characterRunners.Add(runner);
    }

    public void AddToEnemyArea(Character character)
    {
        var runner = CreateRunner(character, enemyArea);
        enemyRunners.Add(runner);
    }

    CharacterRunner CreateRunner(Character character, Transform parent)
    {
        var runner = Instantiate<CharacterRunner>(characterRunnerPrefab, parent.position, Quaternion.identity, parent);
        runner.icon.sprite = character.icon;
        runner.character = character;

        var rt = runner.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0f, 0f);
        rt.anchorMin = new Vector2(0f, .5f);
        rt.anchorMax = new Vector2(0f, .5f);
        rt = null;

        return runner;
    }

    void Update(){
        OrderCharacterRunners(characterRunners);
        OrderCharacterRunners(enemyRunners);
    }

    void OrderCharacterRunners (List<CharacterRunner> characterRunners) {
        var orderedCharacterRunners = characterRunners.OrderBy(x => {
            return x.GetComponent<RectTransform>().anchoredPosition.x;
        }).ToArray();

        for(var i = 0; i < orderedCharacterRunners.Length; i++){
            orderedCharacterRunners[i].transform.SetSiblingIndex(i);
        }

        orderedCharacterRunners = null;
    }
}