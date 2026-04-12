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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interacterSource = aim.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = interacterSource.position + (-interacterSource.right * rayOffsetX);
        Ray ray = new Ray(origin, interacterSource.forward);

        if (Physics.SphereCast(ray, radius, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactingObject = interactObj;

                Item hitObjectInformation = hitInfo.collider.GetComponent<Item>();
                if (hitObjectInformation != null)
                {
                    Debug.Log(hitObjectInformation);
                }
            }
            else
            {
                interactingObject = null;
            }
        }
        else
        {
            interactingObject = null;
        }
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
