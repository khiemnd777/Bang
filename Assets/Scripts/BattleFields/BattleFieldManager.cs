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

    public Character[] playerCharacters;
    public Character[] opponentCharacters;
    [Space]
    public Transform playerField;
    public Transform opponentField;

    [System.NonSerialized]
    public FieldSlot[] playerFieldSlots;
    [System.NonSerialized]
    public FieldSlot[] opponentFieldSlots;

    void Start()
    {
        CreateNewBattle();
    }

    public void CreateNewBattle()
    {
        // Add skill for player's characters (It's a hijack)
        foreach(var character in playerCharacters) {
            character.ClearAllLearnedSkills();
            foreach(var skill in character.skills) {
                character.LearnSkill(skill);
            }
        }

        // Add skill for enemy's characters (It's a hijack)
        foreach(var character in opponentCharacters) {
            character.ClearAllLearnedSkills();
            foreach(var skill in character.skills) {
                character.LearnSkill(skill);
            }
        }

        // Add character into player field slots
        playerFieldSlots = playerField.GetComponentsInChildren<FieldSlot>();
        for (var i = 0; i < playerCharacters.Length; i++)
        {
            playerFieldSlots[i].AddField(playerCharacters[i]);
        }

        // Add character into player field slots
        opponentFieldSlots = opponentField.GetComponentsInChildren<FieldSlot>();
        for (var i = 0; i < playerCharacters.Length; i++)
        {
            opponentFieldSlots[i].AddField(opponentCharacters[i]);
        }
    }
}