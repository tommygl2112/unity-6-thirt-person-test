using UnityEngine;
using StarterAssets;

public class ItemIsnpection : MonoBehaviour
{
    public GameObject player;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs _input;
    private Interact interact;
    public GameObject InspectItemCamera;

    void Awake()
    {
        thirdPersonController = player.GetComponent<ThirdPersonController>();
        interact = player.GetComponent<Interact>();
        _input = player.GetComponent<StarterAssetsInputs>();
    }

    void OnEnable()
    {
        thirdPersonController.enabled = false;
        interact.canInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
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

        InspectItemCamera.SetActive(false);
    }
}
