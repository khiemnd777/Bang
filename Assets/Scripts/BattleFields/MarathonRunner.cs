using System.Collections;
using System.Collections.Generic;
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
        var runner = Instantiate<CharacterRunner>(characterRunnerPrefab, Vector3.zero, Quaternion.identity, parent);
        runner.icon.sprite = character.icon;
        runner.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        // runner.transform.localPosition += new Vector3(100f, 0f, 0f);
        return runner;
    }
}