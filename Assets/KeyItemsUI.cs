using UnityEngine;

public class KeyItemsUI : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject itemPrefab;

    private void Start()
    {
        foreach (var key in Inventory.Instance.doorKeys)
        {
            Instantiate(itemPrefab, content);
        }
    }
}