using UnityEngine;
using StarterAssets;

public class ItemView : MonoBehaviour
{
    public GameObject player;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs _input;
    private Interact interact;
    public Animator stateDrivenCameraAnimator;

    void Start()
    {
        thirdPersonController = player.GetComponent<ThirdPersonController>();
        interact = player.GetComponent<Interact>();
        _input = player.GetComponent<StarterAssetsInputs>();
    }

    public void StartViewItem()
    {
        interact.canInteract = false;
        thirdPersonController.enabled = false;
    }

    public void ExitItemView()
    {     
        if (stateDrivenCameraAnimator.GetBool("ViewItem"))
        {
            stateDrivenCameraAnimator.SetBool("ViewItem", false);
            interact.canInteract = true;
            thirdPersonController.enabled = true;
        }
    }
}
