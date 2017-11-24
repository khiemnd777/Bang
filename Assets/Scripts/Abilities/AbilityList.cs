using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityList : MonoBehaviour
{
    #region Singleton
    static AbilityList list;

    public static AbilityList instance
    {
        get
        {
            if (!list)
            {
                list = FindObjectOfType<AbilityList>();
                if (!list)
                {
                    Debug.LogError("There needs to be one active AbilityList script on a GameObject in your scene.");
                }
                else
                {
                    // Init if exists
                }
            }
            return list;
        }
    }
    #endregion

    public AbilityItem itemPrefab;
    public Transform parent;

    public void AddItem(Ability item)
    {
        var abilityItem = Instantiate<AbilityItem>(itemPrefab, new Vector3(1f, 1f, 0f), Quaternion.identity, parent);
        abilityItem.ability = item;
        abilityItem.HandleTitle();
    }

    public void Clear()
    {
        var items = parent.GetComponentsInChildren<AbilityItem>();
        foreach (var item in items)
        {
            DestroyImmediate(item.gameObject);
        }
    }
}