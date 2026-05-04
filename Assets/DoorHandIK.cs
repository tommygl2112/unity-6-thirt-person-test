using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Collections;

public class DoorHandIK : MonoBehaviour
{
    [Header("Left IK")]
    public TwoBoneIKConstraint leftHandIK;
    public MultiRotationConstraint leftHandRotation;
    public Transform leftHandTarget;

    [Header("Right IK")]
    public TwoBoneIKConstraint rightHandIK;
    public MultiRotationConstraint rightHandRotation;
    public Transform rightHandTarget;

    [Header("Door")]
    public Transform doorHandleTarget;
    public BoxCollider doorSideL;
    private bool doorSideLCollider;
    public BoxCollider doorSideR;
    private bool doorSideRCollider;

    [Header("IK Timing")]
    public float weightUpSpeed = 4f;
    public float weightDownSpeed = 2f;
    public float holdTime = 1.2f;
    public float handOffset = -0.1f;

    [Header("Door Rotation")]
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;

    private Quaternion _closedRotation;
    private Coroutine _doorCoroutine;
    private Coroutine _ikCoroutine;

    [Header("Auto Close")]
    public float closeDistance = 3f;
    private Transform currentPlayer;

    void Start()
    {
        _closedRotation = transform.rotation;
    }

    void Update()
    {
        // AUTO CLOSE
        if (isOpen && currentPlayer != null)
        {
            float distance = Vector3.Distance(transform.position, currentPlayer.position);

            if (distance > closeDistance)
            {
                if (_doorCoroutine != null) StopCoroutine(_doorCoroutine);
                _doorCoroutine = StartCoroutine(ToggleDoor(null));
                currentPlayer = null;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            currentPlayer = other.transform;

            // Detectar lado del jugador
            Vector3 toPlayer = other.transform.position - transform.position;
            float dot = Vector3.Dot(transform.right, toPlayer);

            doorSideLCollider = dot < 0;
            doorSideRCollider = dot > 0;

            // IK
            if (_ikCoroutine != null) StopCoroutine(_ikCoroutine);
            _ikCoroutine = StartCoroutine(HandleIK());

            // PUERTA
            if (_doorCoroutine != null) StopCoroutine(_doorCoroutine);
            _doorCoroutine = StartCoroutine(ToggleDoor(other));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorSideLCollider = false;
            doorSideRCollider = false;
        }
    }

    private IEnumerator ToggleDoor(Collider other)
    {
        float direction = 1f;

        if (other != null)
        {
            Vector3 toPlayer = other.transform.position - transform.position;
            direction = Vector3.Dot(transform.right, toPlayer) > 0 ? 1f : -1f;
        }

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

    private IEnumerator HandleIK()
    {
        float weight = 0f;

        Vector3 offset = doorHandleTarget.forward * handOffset;

        bool useLeft = doorSideLCollider;
        bool useRight = doorSideRCollider;

        // SUBIR
        while (weight < 1f)
        {
            weight += Time.deltaTime * weightUpSpeed;
            weight = Mathf.Clamp01(weight);

            ApplyIK(weight, offset, useLeft, useRight);

            yield return null;
        }

        // MANTENER
        yield return new WaitForSeconds(holdTime);

        // BAJAR
        while (weight > 0f)
        {
            weight -= Time.deltaTime * weightDownSpeed;
            weight = Mathf.Clamp01(weight);

            ApplyIK(weight, offset, useLeft, useRight);

            yield return null;
        }

        ResetIK();
    }

    private void ApplyIK(float weight, Vector3 offset, bool useLeft, bool useRight)
    {
        if (useLeft)
        {
            leftHandTarget.position = doorHandleTarget.position + offset;
            leftHandTarget.rotation = doorHandleTarget.rotation;

            leftHandIK.weight = weight;
            leftHandRotation.weight = weight;
        }
        else if (useRight)
        {
            rightHandTarget.position = doorHandleTarget.position + offset;
            rightHandTarget.rotation = doorHandleTarget.rotation;

            rightHandIK.weight = weight;
            rightHandRotation.weight = weight;
        }
    }

    void ResetIK()
    {
        leftHandIK.weight = 0f;
        leftHandRotation.weight = 0f;
        rightHandIK.weight = 0f;
        rightHandRotation.weight = 0f;
    }
}