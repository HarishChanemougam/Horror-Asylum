using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnIteamChanged();
    public OnIteamChanged onIteamChangedCallBack;
        
        

    public int _space = 20;

    public List<Iteam> iteams = new List<Iteam>(); 

    public bool Add(Iteam iteam)
    {
        if(!iteam._isDefaultIteam)
        {
            if(iteam.Count >= _space)
            {
                Debug.Log("Not enough room");
                return false;
            }
            iteams.Add(iteam);

            if(onIteamChangedCallBack != null)
            {
            onIteamChangedCallBack.Invoke();
            }
        }

        return true;
    }

    public void Remove (Iteam iteam)
    {
        iteam.Remove(iteam);

        if (onIteamChangedCallBack != null)
        {
            onIteamChangedCallBack.Invoke();
        }
    }
}
