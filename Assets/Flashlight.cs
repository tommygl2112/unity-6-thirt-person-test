using UnityEngine;

public class Flashlight : MonoBehaviour, IItemAction
{
    public void Execute(ItemIsnpection itemIsnpection)
    {
        Debug.Log("Linterna obtenida");

        if (itemIsnpection.interact.item.destroyItem)
        {
            Destroy(itemIsnpection.interact.item.gameObject);
        }
    }
}
