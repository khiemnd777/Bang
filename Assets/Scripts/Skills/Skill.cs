using UnityEngine;

public class Skill : MonoBehaviour
{
    new public string name = "New Skill";
    public Sprite icon;
    public Character owner;
    
    public virtual int[] Use() 
    {
        Debug.Log("Using skill " + name);
        return new int[0];
    }
}