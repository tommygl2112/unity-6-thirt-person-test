using UnityEngine;
using StarterAssets;
using TMPro;

public interface IItemAction // interact.item.destroyItem
{
    void Execute(ItemIsnpection itemIsnpection);
}

public class ItemIsnpection : MonoBehaviour
{
    public GameObject player;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs _input;
    public Interact interact;
    public GameObject InspectItemCamera;
    public MeshRenderer inspectedItemMeshRenderer; // Item.cs
    public Camera interactUiCamera;
    public SkinnedMeshRenderer playerMeshRenderer;
    public GameObject ReadItemCamera;
    public bool isReading;
    public TextMeshProUGUI itemInspectedTextMesh; // Item.cs
    public TextMeshProUGUI readItemTextMesh;
    public Animator stateDrivenCameraAnimator;
    public GameObject inspectUI;
    public GameObject readItemText_ButtonTMP;
    public GameObject exitItemInspection_ButtonTMP;
    public GameObject rotateItemText_ButtonTMP;

    void Awake()
    {
        thirdPersonController = player.GetComponent<ThirdPersonController>();
        interact = player.GetComponent<Interact>();
        _input = player.GetComponent<StarterAssetsInputs>();
    }

    void OnEnable()
    {
        interactUiCamera.enabled = false;
        thirdPersonController.enabled = false;
        interact.canInteract = false;
        playerMeshRenderer.enabled = false;

        inspectUI.SetActive(true);
        
        readItemTextMesh.text = itemInspectedTextMesh.text;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
        readItemText_ButtonTMP.GetComponent<TextMeshProUGUI>().text = UiControllelButtons.GetReadItemTextUiButton();
        exitItemInspection_ButtonTMP.GetComponent<TextMeshProUGUI>().text = UiControllelButtons.GetExitItemInspectiontUiButton();
        rotateItemText_ButtonTMP.GetComponent<TextMeshProUGUI>().text = UiControllelButtons.GetLookUiButton();
    }

    private void CameraRotation()
    {
        if(isReading){return;}

        // if there is an input and camera position is not fixed
        if (_input.look.sqrMagnitude >= 0.01f)
        {
            float rotationSpeed = 100f;
            float mouseX = _input.look.x;
            float mouseY = _input.look.y;

            // Rotación horizontal (izquierda/derecha)
            transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime, Space.World);
            // Rotación vertical (arriba/abajo)
            transform.Rotate(Vector3.right, -mouseY * rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    public void ExitItemInspection()
    {     
        thirdPersonController.enabled = true;
        interact.canInteract = true;

        inspectedItemMeshRenderer.enabled = true;
        playerMeshRenderer.enabled = true;
        InspectItemCamera.SetActive(false);
        interactUiCamera.enabled = true;
        inspectUI.SetActive(false);

        stateDrivenCameraAnimator.SetBool("ViewItem", false);

        if (interact.item.destroyItem)
        {
            interact.item.GetComponent<IItemAction>()?.Execute(this);
            // Destroy(interact.item.gameObject); se destruye en IItemAction.Execute()
        }
    }

    public void ReadText()
    {
        isReading = true;
        ReadItemCamera.SetActive(true);
    }

    public void StopReading()
    {
        isReading = false;
        ReadItemCamera.SetActive(false);
    }
}
