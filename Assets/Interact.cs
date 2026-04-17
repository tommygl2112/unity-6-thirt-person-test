using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public interface IInteractable
{
    void Interact();
}

public class Interact : MonoBehaviour
{
    public GameObject aim;
    private Transform interacterSource;
    public float interactRange = 1f;
    public IInteractable interactingObject;
    public float radius = 0.5f;
    public float rayOffsetX;
    public bool canInteract = true; // Text.cs, StarterAssetsInputs.cs
    public GameObject interactItemUi;
    public GameObject foundedItemUi;
    public Item item;
    public float detectItemRadius;
    public float detectItemRange;
    public LayerMask interactLayerMask;
    public Dictionary<Item, GameObject> activeItemUIs = new Dictionary<Item, GameObject>();
    public GameObject interactUiCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interacterSource = aim.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            //interactuar con items
            IInteractable foundInteractable = null;
            Item foundItem = null;

            Vector3 origin = interacterSource.position + (-interacterSource.right * rayOffsetX);
            Ray ray = new Ray(origin, interacterSource.forward); // usar items

            // interactuar con items
            RaycastHit[] interactHits = Physics.SphereCastAll(ray, radius, interactRange, interactLayerMask);
            var sortedInteractHits = interactHits.OrderBy(h => h.distance);

            foreach (var hit in sortedInteractHits)
            {
                if (hit.collider.TryGetComponent(out IInteractable interactObj))
                {
                    foundInteractable = interactObj;
                    foundItem = hit.collider.GetComponent<Item>();
                    break; // opcional: parar en el primero
                }
            }
            if (foundInteractable != null)
            {
                interactingObject = foundInteractable;
                item = foundItem;

                if (item != null)
                {
                    if (activeItemUIs.ContainsKey(foundItem))
                    {
                        Destroy(activeItemUIs[foundItem]);
                        activeItemUIs.Remove(foundItem);
                    }

                    RectTransform uiRect = interactItemUi.GetComponent<RectTransform>();
                    uiRect.position = item.itemInteractUiPosition.position;

                    interactItemUi.SetActive(true);
                }
            }
            else
            {
                ClearInteractInformation();
            }

            // buscar items
            Item foundItem2 = null;
            List<Item> foundItems = new List<Item>();

            RaycastHit[] detectHits = Physics.SphereCastAll(ray, detectItemRadius, detectItemRange, interactLayerMask);
            var sortedHits = detectHits.OrderBy(h => h.distance);

            foreach (var hit in sortedHits)
            {
                if (hit.collider.TryGetComponent(out IInteractable interactObj))
                {
                    foundItem2 = hit.collider.GetComponent<Item>();

                    // 🔥 IGNORAR el item que ya estás interactuando
                    if (foundItem2 != null && foundItem2 != foundItem)
                    {
                        foundItems.Add(foundItem2);
                    }
                }
            }

            // crear UI para cada item
            HashSet<Item> currentFrameItems = new HashSet<Item>();
            foreach (var itm in foundItems)
            {
                currentFrameItems.Add(itm);

                // SI YA EXISTE → solo actualizar posición
                if (activeItemUIs.ContainsKey(itm))
                {
                    GameObject ui = activeItemUIs[itm];
                    RectTransform uiRect = ui.GetComponent<RectTransform>();

                    Vector3 screenPos = itm.itemInteractUiPosition.position;

                    if (screenPos.z < 0)
                    {
                        ui.SetActive(false);
                        continue;
                    }

                    ui.SetActive(true);
                    uiRect.position = screenPos;
                }
                else
                {
                    // NO EXISTE → crear uno nuevo
                    GameObject ui = Instantiate(foundedItemUi, interactUiCanvas.transform);
                    RectTransform uiRect = ui.GetComponent<RectTransform>();

                    uiRect.position = itm.itemInteractUiPosition.position;

                    ui.SetActive(true);

                    activeItemUIs.Add(itm, ui);
                }
            }  

            List<Item> itemsToRemove = new List<Item>();
            foreach (var kvp in activeItemUIs)
            {
                if (!currentFrameItems.Contains(kvp.Key))
                {
                    Destroy(kvp.Value);
                    itemsToRemove.Add(kvp.Key);
                }
            }

            foreach (var itm in itemsToRemove)
            {
                activeItemUIs.Remove(itm);
            }
            
        }
        else
        {
            // foundedItemUi.SetActive(false);
            interactItemUi.SetActive(false);
        }

    }

    private void ClearInteractInformation()
    {
        interactingObject = null;
        interactItemUi.SetActive(false);
        item = null;
    }

    //debug
    private void OnDrawGizmos()
    {
        if (aim == null) return;

        if (interacterSource == null)
        {
            interacterSource = aim.GetComponent<Transform>();
        }

        Vector3 origin = interacterSource.position + (-interacterSource.right * rayOffsetX);
        Vector3 endPoint = origin + interacterSource.forward * interactRange;
        Vector3 endPoint2 = origin + interacterSource.forward * detectItemRange;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, endPoint);
        Gizmos.DrawWireSphere(origin, radius);
        Gizmos.DrawWireSphere(endPoint, radius);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, endPoint2);
        Gizmos.DrawWireSphere(origin, detectItemRadius);
        Gizmos.DrawWireSphere(endPoint2, detectItemRadius);
    }
}
