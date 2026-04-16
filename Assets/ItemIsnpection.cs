using UnityEngine;
using StarterAssets;

public class ItemIsnpection : MonoBehaviour
{
    public GameObject player;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs _input;
    private Interact interact;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thirdPersonController = player.GetComponent<ThirdPersonController>();
        _input = player.GetComponent<StarterAssetsInputs>();
        interact = player.GetComponent<Interact>();

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
}
