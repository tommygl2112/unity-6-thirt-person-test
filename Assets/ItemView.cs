using UnityEngine;
using StarterAssets;
using TMPro;

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
    public GameObject viewUI;
    public GameObject viewUI_ButtonTMP;

    void Start()
    {
        thirdPersonController = player.GetComponent<ThirdPersonController>();
        interact = player.GetComponent<Interact>();
        _input = player.GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (viewUI_ButtonTMP != null)
        {
            viewUI_ButtonTMP.GetComponent<TextMeshProUGUI>().text = UiControllelButtons.GetExitItemViewUiButton();
        }
    }

    public void StartViewItem(bool useKeyItem) // Item.cs
    {
        interactUiCamera.enabled = false;
        viewUI.SetActive(true);
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
            viewUI.SetActive(false);
            interactUiCamera.enabled = true;
            interact.canInteract = true;
            thirdPersonController.enabled = true;

            if (useKeyItemUi != null && useKeyItemUi.activeSelf)
            {
                useKeyItemUi.SetActive(false);
            }
        }
    }
}
