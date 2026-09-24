using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Item collected.");

        if (DoorUnlock.Instance != null)
            DoorUnlock.Instance.CollectItem();

        Destroy(gameObject);
    }
}