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
    public Character[] opponentCharacters;

    public Transform field;
    public Transform opponentField;
    [Space]
    [System.NonSerialized]
    public FieldSlot[] fieldSlots;
    [System.NonSerialized]
    public FieldSlot[] opponentFieldSlots;

    void Start()
    {
        CreateNewBattle();
    }

    public void CreateNewBattle()
    {
        // Player field slots
        fieldSlots = field.GetComponentsInChildren<FieldSlot>();
        for (var i = 0; i < characters.Length; i++)
        {
            fieldSlots[i].AddField(characters[i]);
        }

        // Opponent field slots
        opponentFieldSlots = opponentField.GetComponentsInChildren<FieldSlot>();
        for (var i = 0; i < opponentCharacters.Length; i++)
        {
            opponentFieldSlots[i].AddField(opponentCharacters[i]);
        }
    }
}