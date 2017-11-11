using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    static EquipmentManager manager;

    public static EquipmentManager instance
    {
        get
        {
            manager = FindObjectOfType<EquipmentManager>();
            if (manager == null)
            {
                Debug.LogError("There needs to be one active EquipmentManager script on a GameObject in your scene.");
            }
            else
            {
                // Init EquipmentManager if it exists
            }
            return manager;
        }
    }
    #endregion

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Equipment[] currentEquipment;

    void Start()
    {
        var numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

    public void Equip(Equipment item)
    {
        var slotIndex = (int)item.equipSlot;
        Equipment oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            Inventory.instance.Add(oldItem);
        }

        if(onEquipmentChanged != null)
            onEquipmentChanged.Invoke(item, oldItem);

        currentEquipment[slotIndex] = item;
    }

    public void Unequip(EquipmentSlot slot)
    {
        var slotIndex = (int)slot;
        var currentItem = currentEquipment[slotIndex];
        if (currentItem == null)
            return;
        Inventory.instance.Add(currentItem);
        currentEquipment[slotIndex] = null;

        if(onEquipmentChanged != null)
            onEquipmentChanged.Invoke(null, currentItem);
    }

    public void UnequipAll()
    {
        for (var i = 0; i < currentEquipment.Length; i++)
        {
            Unequip((EquipmentSlot)i);
        }
    }
}