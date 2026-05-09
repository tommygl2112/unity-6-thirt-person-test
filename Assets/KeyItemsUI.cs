using UnityEngine;

public class KeyItemsUI : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;

    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var key in Inventory.Instance.doorKeys)
        {
            Instantiate(itemPrefab, transform);
        }
    }
}