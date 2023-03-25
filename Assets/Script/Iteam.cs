using NaughtyAttributes;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Iteam", menuName ="Inventory/Iteam")]
public class Iteam : ScriptableObject
{
    public string _name = "New Iteam";
    public Sprite _icon = null;
    public bool _isDefaultIteam = false;

    public virtual void Use()
    {
        //use iteam
        Debug.Log("Using" + name);
    }

    public int Count { get; internal set; }

    internal void Remove(Iteam iteam)
    {
        Debug.Log("Remove");
    }
}
