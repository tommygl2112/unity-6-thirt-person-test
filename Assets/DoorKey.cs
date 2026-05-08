using UnityEngine;

public class DoorKey : MonoBehaviour, IItemAction
{
    public void Execute(ItemIsnpection itemIsnpection) // pick up
    {
        Debug.Log("Door Key obtenida");

        if (itemIsnpection.interact.item.destroyItem)
        {
            Destroy(itemIsnpection.interact.item.gameObject);
        }
    }
}
