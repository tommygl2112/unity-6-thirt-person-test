using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public string itemName;

    public void Interact()
    {
        Debug.Log(itemName);
    }
}
