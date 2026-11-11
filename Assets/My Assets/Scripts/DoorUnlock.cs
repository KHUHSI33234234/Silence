
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public static DoorUnlock Instance;

    public int itemsRequired = 2;
    private int itemsCollected = 0;
    private bool puzzleCompleted = false;

    public DoorController firstDoor;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectItem()
    {
        if (puzzleCompleted) return;

        itemsCollected++;

        Debug.Log("Items collected: " +
            itemsCollected + " / " + itemsRequired);

        if (itemsCollected >= itemsRequired)
        {
            puzzleCompleted = true;

            Debug.Log("First puzzle completed!");

            if (firstDoor != null)
            {
                firstDoor.OpenDoor(() =>
                {
                    GameManager.Instance.CompletePuzzle(1);
                });
            }
            else
            {
                Debug.LogError("First door is not assigned!");
            }
        }
    }
}
