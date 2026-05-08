using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [Header("Inventory")]
    public bool flashlight;
    public List<DoorKeyData> doorKeys = new();

    private void Awake()
    {
        Instance = this;
    }

    // save items =========================================
    public void AddFlashlight()
    {
        flashlight = true;
    }

    public void Add(DoorKey key)
    {
        DoorKeyData data = new DoorKeyData
        {
            doorId = key.doorId,
            keyName = key.keyName,
            consumed = false
        };

        doorKeys.Add(data);
    }
}