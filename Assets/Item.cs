using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public string itemName;

    public Transform itemInteractUiPosition;
    public bool dialogueAction; //configurar en el editor
    public Text text;
    public bool inspectItemAction;
    public GameObject inspectItemCamera;
    public GameObject itemInspected; // -> ItemInspection.cs
    public ItemIsnpection itemIsnpection;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    public bool destroyItem;
    public bool viewItem;
    public Animator StateDrivenCameraAnimator;
    public GameObject viewItemCamera;
    public Transform itemViewPosition;

    void Start()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    public void Interact()
    {
        //dialogue
        if (dialogueAction)
        {
            if (!text.isDialogueActive)
            {
                text.dialogueName = itemName;
                text.lines = new string[]{"texto de prueba", "texto de prueba 2"};
                text.StartDialogue();
            }
        }
        else if (inspectItemAction)
        {
            itemIsnpection = itemInspected.GetComponent<ItemIsnpection>();

            // Obtener componentes del objeto inspeccionado
            MeshFilter inspectedFilter = itemInspected.GetComponent<MeshFilter>();
            MeshRenderer inspectedRenderer = itemInspected.GetComponent<MeshRenderer>();

            // Copiar mesh y material
            inspectedFilter.mesh = meshFilter.mesh;
            inspectedRenderer.material = meshRenderer.material;

            inspectItemCamera.SetActive(true);
        }
        else if (viewItem)
        {
            Debug.Log("viewItem");

            Transform target = viewItemCamera.transform;
            Transform source = itemViewPosition;

            target.position = source.position;
            target.rotation = source.rotation;
            target.localScale = source.localScale;
            
            StateDrivenCameraAnimator.SetBool("ViewItem", true); // el animator controla que camara mostrar en State-Driven Camera
        }
    }
}
