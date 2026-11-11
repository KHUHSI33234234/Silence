
using UnityEngine;
using System;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public Transform doorMesh;

    public Vector3 hingeOffset = new Vector3(-0.5f, 0f, 0f);
    public float openAngle = 90f;
    public float openDuration = 1.2f;

    private Transform pivot;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private bool isOpen;
    private bool isAnimating;

    void Start()
    {
        if (doorMesh == null)
        {
            Debug.LogError("Door Mesh is not assigned!");
            return;
        }

        originalPosition = doorMesh.position;
        originalRotation = doorMesh.rotation;

        GameObject pivotObject = new GameObject("RuntimeDoorPivot");
        pivot = pivotObject.transform;

        pivot.position = doorMesh.TransformPoint(hingeOffset);
        pivot.rotation = originalRotation;

        pivot.SetParent(transform, true);
    }

    public void OpenDoor(Action onOpened = null)
    {
        if (pivot == null || isOpen || isAnimating)
            return;

        StartCoroutine(OpenAnimation(onOpened));
    }

    IEnumerator OpenAnimation(Action onOpened)
    {
        isAnimating = true;

        Quaternion startRotation = pivot.rotation;

        Quaternion targetRotation =
            startRotation * Quaternion.Euler(0f, openAngle, 0f);

        float elapsed = 0f;

        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / openDuration);
            float smoothT = t * t * (3f - 2f * t);

            Quaternion currentRotation =
                Quaternion.Slerp(startRotation, targetRotation, smoothT);

            pivot.rotation = currentRotation;

            doorMesh.position = pivot.position +
                currentRotation * (originalPosition - pivot.position);

            doorMesh.rotation =
                currentRotation * Quaternion.Inverse(startRotation)
                * originalRotation;

            yield return null;
        }

        pivot.rotation = targetRotation;

        doorMesh.position = pivot.position +
            targetRotation * (originalPosition - pivot.position);

        doorMesh.rotation =
            targetRotation * Quaternion.Inverse(startRotation)
            * originalRotation;

        isOpen = true;
        isAnimating = false;

        Debug.Log("Door opened!");

        onOpened?.Invoke();
    }
}
