using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public string itemName;

    public Transform itemInteractUiPosition;
    public bool dialogueAction; //configurar en el editor
    public Text text;
    public bool inspectItemAction;
    public GameObject inspectItemCamera;
    public GameObject itemInspected;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

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
            Debug.Log("inspectItemAction");

            // Obtener componentes del objeto inspeccionado
            MeshFilter inspectedFilter = itemInspected.GetComponent<MeshFilter>();
            MeshRenderer inspectedRenderer = itemInspected.GetComponent<MeshRenderer>();

            // Copiar mesh y material
            inspectedFilter.mesh = meshFilter.mesh;
            inspectedRenderer.material = meshRenderer.material;

            inspectItemCamera.SetActive(true);
            Destroy(gameObject);
        }
    }
}
