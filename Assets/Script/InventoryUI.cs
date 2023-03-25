using NaughtyAttributes;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] public Transform _iteamsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    InventorySlot[] slot;
    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onIteamChangedCallBack += UpdateUI;

        slot = _iteamsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
       for(int i = 0; i < slot.Length; i++)
        {
            if(i < inventory.iteams.Count)
            {
                slot[i].AddIteam(inventory.iteams[i]);
            }
            else
            {
                slot[i].ClearSlot();
            }
        }
    }
}
