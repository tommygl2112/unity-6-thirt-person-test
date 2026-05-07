using UnityEngine;

public class FlashlightFollow : MonoBehaviour
{
    public Transform cameraTarget;

    [Header("Follow")]
    [SerializeField] private float positionSpeed = 20f;
    [SerializeField] private float rotationSpeed = 8f;

    [SerializeField] private Vector3 positionOffset;

    void LateUpdate()
    {
        FollowPosition();
        FollowRotation();
    }

    void FollowPosition()
    {
        Vector3 targetPosition =
            cameraTarget.position +
            cameraTarget.TransformDirection(positionOffset);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            positionSpeed * Time.deltaTime
        );
    }

    void FollowRotation()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            cameraTarget.rotation,
            rotationSpeed * Time.deltaTime
        );
    }
}