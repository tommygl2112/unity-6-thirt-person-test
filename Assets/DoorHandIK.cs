using UnityEngine;
using UnityEngine.Animations.Rigging;

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


    [Header("Puerta")]
    public Transform doorHandleTarget;
    public HingeJoint hinge;
    public BoxCollider doorSideL;
    private bool doorSideLCollider;
    public BoxCollider doorSideR;
    private bool doorSideRCollider;

    [Header("Config")]
    public float blendSpeed = 5f;

    // Cuántos grados debe moverse para considerar "empujando"
    public float pushThreshold = 5f;

    // Velocidad mínima para considerar que la puerta sigue moviéndose
    public float movementThreshold = 1f;

    float targetWeight;

    Quaternion startRotation;
    float lastAngle;

    bool isFollowing = false;


    void Start()
    {
        startRotation = transform.localRotation;
        lastAngle = 0f;
    }

    void Update()
    {
        // Ángulo actual de la puerta
        float angle = Quaternion.Angle(
            startRotation,
            transform.localRotation
        );

        // Velocidad angular (aproximada)
        float angularSpeed = Mathf.Abs(angle - lastAngle) / Time.deltaTime;

        // Detectar inicio de empuje
        if (angle > pushThreshold)
        {
            isFollowing = true;
        }

        // Si la puerta sigue moviéndose, mantener la mano
        if (isFollowing && angularSpeed > movementThreshold)
        {
            targetWeight = 1f;

            // La mano sigue SIEMPRE al picaporte
            if (doorSideLCollider)
            {
                leftHandTarget.position = doorHandleTarget.position;
                leftHandTarget.rotation = doorHandleTarget.rotation;
            }
            else if (doorSideRCollider)
            {
                rightHandTarget.position = doorHandleTarget.position;
                rightHandTarget.rotation = doorHandleTarget.rotation;
            }
        }
        else
        {
            // Cuando se detiene, soltar
            isFollowing = false;
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
        if (other.CompareTag("Player"))
        {
            if (other.bounds.Intersects(doorSideL.bounds))
            {
                ResetIK();
                doorSideLCollider = true;
                doorSideRCollider = false;
            }

            if (other.bounds.Intersects(doorSideR.bounds))
            {
                ResetIK();
                doorSideRCollider = true;
                doorSideLCollider = false;
            }
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

    isFollowing = false;
    targetWeight = 0f;
}
}