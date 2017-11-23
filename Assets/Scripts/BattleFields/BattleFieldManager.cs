using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldManager : MonoBehaviour
{

    #region Singleton

    static BattleFieldManager manager;
    public static BattleFieldManager instance
    {
        get
        {
            manager = FindObjectOfType<BattleFieldManager>();
            if (manager == null)
            {
                Debug.LogError("There needs to be one active BattleFieldManager script on a GameObject in your scene.");
            }
            else
            {
                // If it exists then init
            }
            return manager;
        }
    }

    #endregion

    public int numOfFields = 9;

    public Character[] characters;
    [Space]
    public Transform playerField;
    public Transform opponentField;
    [Space]
    public MarathonRunner marathonRunner;

    [System.NonSerialized]
    public FieldSlot[] playerFieldSlots;
    [System.NonSerialized]
    public FieldSlot[] opponentFieldSlots;

    void Start()
    {
        CreateNewBattle();
    }

    void Update()
    {
        Run();
    }

    void Run()
    {
        // if character is in a turn
        if(marathonRunner.isStopped)
            return;
        var singleCharacterInTurn = characters.FirstOrDefault(x => x.isTurn);
        if(singleCharacterInTurn != null){
            Debug.Log(singleCharacterInTurn.name + " has been turned!");
            marathonRunner.StopRunner();
            singleCharacterInTurn.HandleAbilities();
        }
    }

    public void CreateNewBattle()
    {
        // Add skill for player's characters (It's a hijack)
        foreach (var character in characters)
        {
            character.ClearAllLearnedSkills();
            character.ClearAllTactics();
            foreach (var skill in character.skills)
            {
                var learnedSkill = character.LearnSkill(skill);
                character.AddAbility(learnedSkill);
            }
            if (!character.isEnemy)
            {
                marathonRunner.AddToCharacterArea(character);
            }
            else
            {
                marathonRunner.AddToEnemyArea(character);
            }
        }

        // Add character into player field slots
        playerFieldSlots = playerField.GetComponentsInChildren<FieldSlot>();
        var playerCharacters = characters.Where(x => !x.isEnemy).ToArray();
        for (var i = 0; i < playerCharacters.Count(); i++)
        {
            playerFieldSlots[i].AddField(playerCharacters[i]);
        }
        playerCharacters = null;

        // Add enemy into enemy field slots
        opponentFieldSlots = opponentField.GetComponentsInChildren<FieldSlot>();
        var opponentCharacters = characters.Where(x => x.isEnemy).ToArray();
        for (var i = 0; i < opponentCharacters.Count(); i++)
        {
            opponentFieldSlots[i].AddField(opponentCharacters[i]);
        }
        opponentCharacters = null;
    }
}