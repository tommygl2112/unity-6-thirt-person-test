using UnityEngine;

public class DoorRange : MonoBehaviour
{
    public DoorHandIK door;

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.CloseDoor();
        }
    }
}