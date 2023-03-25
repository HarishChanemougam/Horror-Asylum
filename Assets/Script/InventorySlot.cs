using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image _icon;
    public Button _removeButton;
    Iteam iteam;

    public void AddIteam(Iteam newIteam)
    {
        iteam = newIteam;
        _icon.sprite = iteam._icon;
        _icon.enabled = true;
        _removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        iteam = null;
        _icon.sprite = null;
        _icon.enabled = false;
        _removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(iteam);
    }

    public void UseIteam()
    {
        if(iteam != null)
        {
            iteam.Use();
        }
    }
}
