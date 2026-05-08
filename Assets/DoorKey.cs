using UnityEngine;
using System;

[Serializable]
public class DoorKeyData
{
    public string doorId;
    public string keyName;
    public bool consumed;
}

public class DoorKey : MonoBehaviour, IItemAction
{
    public string doorId;
    public string keyName;
    
    public void Execute(ItemIsnpection itemIsnpection) // pick up
    {
        Debug.Log("Door Key obtenida");

        Inventory.Instance.Add(this);

        if (itemIsnpection.interact.item.destroyItem)
        {
            Destroy(itemIsnpection.interact.item.gameObject);
        }
    }
}
