using NaughtyAttributes;
using UnityEngine;

public class IteamPickup : Interactable
{
    public Iteam _iteam;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up" + _iteam._name);
       bool wasPickedUp =  Inventory.instance.Add(_iteam);

        if (wasPickedUp)
        {
        Destroy(gameObject);
        }
    }
}
