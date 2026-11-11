
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public AudioClip pickupSound;
    public float soundVolume = 1f;

    private bool collected = false;

    void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player"))
            return;

        collected = true;

        Debug.Log("Item collected.");

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(
                pickupSound,
                transform.position,
                soundVolume
            );
        }

        if (DoorUnlock.Instance != null)
        {
            DoorUnlock.Instance.CollectItem();
        }

        Destroy(gameObject);
    }
}
