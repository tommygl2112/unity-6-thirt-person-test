using UnityEngine;
using StarterAssets;

public class ItemIsnpection : MonoBehaviour
{
    public ThirdPersonController thirdPersonController;
    public Interact interact;

    void Awake()
    {
        thirdPersonController.enabled = false;
        interact.canInteract = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
