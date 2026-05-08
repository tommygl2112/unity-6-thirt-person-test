using UnityEngine;

public class Flashlight : MonoBehaviour, IItemAction
{
    public GameObject playerFlashlight;
    public GameObject flashlightSpot;

    public void Execute(ItemIsnpection itemIsnpection) // pick up
    {
        Debug.Log("Linterna obtenida");

        playerFlashlight.SetActive(true);
        flashlightSpot.SetActive(true);

        Inventory.Instance.AddFlashlight();

        if (itemIsnpection.interact.item.destroyItem)
        {
            Destroy(itemIsnpection.interact.item.gameObject);
        }
    }

    public void UseFlashlight()
    {
        if (flashlightSpot.activeSelf)
        {
            flashlightSpot.SetActive(false);
        }
        else
        {
            flashlightSpot.SetActive(true);
        }
    }
}
