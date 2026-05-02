using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Collections;
using StarterAssets;


public class DoorHandIK : MonoBehaviour
{
    [Header("Left IK")]
    public TwoBoneIKConstraint leftHandIK;
    public MultiRotationConstraint leftHandRotation;
    public Transform leftHandTarget;

    [Header("RightIK")]
    public TwoBoneIKConstraint rightHandIK;
    public MultiRotationConstraint rightHandRotation;
    public Transform rightHandTarget;


    [Header("Door")]
    public Transform doorHandleTarget;
    public BoxCollider doorSideL;
    private bool doorSideLCollider;
    public BoxCollider doorSideR;
    private bool doorSideRCollider;

    [Header("Config")]
    public float blendSpeed = 5f;

    float targetWeight;

    Quaternion startRotation;
    float lastAngle;
    public float handOffset = -0.1f;

    [Header("Door Rotation")]
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _currentCoroutine;

    public StarterAssetsInputs _input;

    void Start()
    {
        startRotation = transform.localRotation;
        lastAngle = 0f;

        _closedRotation = transform.rotation;
        _openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        // Ángulo actual de la puerta
        float angle = Quaternion.Angle(
            startRotation,
            transform.localRotation
        );

        // Si la puerta sigue moviéndose, mantener la mano
        if (doorSideLCollider || doorSideRCollider)
        {
            targetWeight = 1f;
            Vector3 offset = doorHandleTarget.forward * handOffset;

            // La mano sigue SIEMPRE al picaporte
            if (doorSideLCollider)
            {
                leftHandTarget.position = doorHandleTarget.position + offset;
                leftHandTarget.rotation = doorHandleTarget.rotation;
            }
            else if (doorSideRCollider)
            {
                rightHandTarget.position = doorHandleTarget.position + offset;
                rightHandTarget.rotation = doorHandleTarget.rotation;
            }
        }
        else
        {
            targetWeight = 0f;
        }

        // Guardar para el siguiente frame
        lastAngle = angle;

        if (doorSideLCollider)
        {
            // Blend suave del IK
            leftHandIK.weight = Mathf.Lerp(
                leftHandIK.weight,
                targetWeight,
                Time.deltaTime * blendSpeed
            );

            leftHandRotation.weight = Mathf.Lerp(
                leftHandRotation.weight,
                targetWeight,
                Time.deltaTime * blendSpeed
            );
        }
        else if (doorSideRCollider)
        {
            // Blend suave del IK
            rightHandIK.weight = Mathf.Lerp(
                rightHandIK.weight,
                targetWeight,
                Time.deltaTime * blendSpeed
            );

            rightHandRotation.weight = Mathf.Lerp(
                rightHandRotation.weight,
                targetWeight,
                Time.deltaTime * blendSpeed
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            ResetIK();

            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(ToggleDoor(other));
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorSideLCollider = false;
            doorSideRCollider = false;
            ResetIK();
        }
    }

    void ResetIK()
    {
        // Poner weights en 0 inmediatamente
        leftHandIK.weight = 0f;
        leftHandRotation.weight = 0f;
        rightHandIK.weight = 0f;
        rightHandRotation.weight = 0f;

        // Resetear posición/rotación de targets al rig original (ej: huesos)
        leftHandTarget.localPosition = Vector3.zero;
        leftHandTarget.localRotation = Quaternion.identity;

        rightHandTarget.localPosition = Vector3.zero;
        rightHandTarget.localRotation = Quaternion.identity;

        targetWeight = 0f;
    }

private IEnumerator ToggleDoor(Collider other)
{
    Vector3 toPlayer = other.transform.position - transform.position;
    float direction = Vector3.Dot(transform.right, toPlayer) > 0 ? 1f : -1f;

    Quaternion targetRotation;

    if (!isOpen)
    {
        targetRotation = Quaternion.Euler(
            transform.eulerAngles + new Vector3(0, openAngle * direction, 0)
        );
    }
    else
    {
        targetRotation = _closedRotation;
    }

    isOpen = !isOpen;

    while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
    {
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * openSpeed
        );

        yield return null;
    }

    transform.rotation = targetRotation;
}
}