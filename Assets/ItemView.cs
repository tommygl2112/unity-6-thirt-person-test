using UnityEngine;
using StarterAssets;

public class ItemView : MonoBehaviour
{
    public GameObject player;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs _input;
    private Interact interact;
    public Animator stateDrivenCameraAnimator;
    public Camera interactUiCamera;
    public SkinnedMeshRenderer playerMeshRenderer;

    void Start()
    {
        thirdPersonController = player.GetComponent<ThirdPersonController>();
        interact = player.GetComponent<Interact>();
        _input = player.GetComponent<StarterAssetsInputs>();
    }

    public void StartViewItem()
    {
        interactUiCamera.enabled = false;
        interact.canInteract = false;
        thirdPersonController.enabled = false;
        playerMeshRenderer.enabled = false;
    }

    public void ExitItemView()
    {     
        if (stateDrivenCameraAnimator.GetBool("ViewItem"))
        {
            playerMeshRenderer.enabled = true;
            stateDrivenCameraAnimator.SetBool("ViewItem", false);
            interactUiCamera.enabled = true;
            interact.canInteract = true;
            thirdPersonController.enabled = true;
        }
    }
}
