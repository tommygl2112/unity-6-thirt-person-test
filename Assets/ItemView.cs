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
    public GameObject useKeyItemUi;
    public GameObject noItemsText;
    public GameObject keyItemsSelectionScrollView;

    void Start()
    {
        thirdPersonController = player.GetComponent<ThirdPersonController>();
        interact = player.GetComponent<Interact>();
        _input = player.GetComponent<StarterAssetsInputs>();
    }

    public void StartViewItem(bool useKeyItem) // Item.cs
    {
        interactUiCamera.enabled = false;
        interact.canInteract = false;
        thirdPersonController.enabled = false;
        playerMeshRenderer.enabled = false;

        if (useKeyItem)
        {
            useKeyItemUi.SetActive(true);

            if (Inventory.Instance.doorKeys != null && Inventory.Instance.doorKeys.Count > 0)
            {
                // Tiene elementos
                noItemsText.SetActive(false);
                keyItemsSelectionScrollView.SetActive(true);
            }
            else
            {
                keyItemsSelectionScrollView.SetActive(false);
                noItemsText.SetActive(true);
            }
        }
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

            if (useKeyItemUi.activeSelf)
            {
                useKeyItemUi.SetActive(false);
            }
        }
    }
}
