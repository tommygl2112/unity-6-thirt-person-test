using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class Interact : MonoBehaviour
{
    public GameObject aim;
    private Transform interacterSource;
    public float InteractRange;
    public IInteractable interactingObject;
    public float radius = 0.5f;
    public float rayOffsetX;
    public bool canInteract = true; // Text.cs, StarterAssetsInputs.cs
    public GameObject interactItemUi;
    public GameObject foundedItemUi;

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
            Vector3 origin = interacterSource.position + (-interacterSource.right * rayOffsetX);
            Ray ray = new Ray(origin, interacterSource.forward); // usar items
            Ray detectionRay = new Ray(origin, interacterSource.forward); // detectar items

            if (Physics.SphereCast(ray, radius, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactingObject = interactObj; // StarterAssetsInputs.cs

                    Item item = hitInfo.collider.GetComponent<Item>();
                    if (item != null)
                    {
                        RectTransform uiRect = interactItemUi.GetComponent<RectTransform>();
                        uiRect.position = item.itemInteractUiPosition.position;   
                        foundedItemUi.SetActive(false);
                        interactItemUi.SetActive(true);
                    }
                }
                else
                {
                    ClearInteractInformation();
                }
            }
            else
            {
                ClearInteractInformation();

                // buscar items
                if (Physics.SphereCast(detectionRay, radius * 2f, out RaycastHit detectionHitInfo, InteractRange * 2f))
                {
                    if (detectionHitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                    {
                        Item item = detectionHitInfo.collider.GetComponent<Item>();
                        if (item != null)
                        {
                            RectTransform uiRect = foundedItemUi.GetComponent<RectTransform>();
                            uiRect.position = item.itemInteractUiPosition.position; 
                            foundedItemUi.SetActive(true);
                        }
                    }
                    else
                    {
                        foundedItemUi.SetActive(false);
                    }
                }
                else
                {
                    foundedItemUi.SetActive(false);
                }
            }
        }
        else
        {
            foundedItemUi.SetActive(false);
            interactItemUi.SetActive(false);
        }

    }

    private void ClearInteractInformation()
    {
        interactingObject = null;
        interactItemUi.SetActive(false);
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
        Vector3 endPoint = origin + interacterSource.forward * InteractRange;

        Gizmos.color = Color.red;

        Gizmos.DrawLine(origin, endPoint);
        Gizmos.DrawWireSphere(origin, radius);
        Gizmos.DrawWireSphere(endPoint, radius);
    }
}
