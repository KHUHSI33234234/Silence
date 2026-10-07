using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public static DoorUnlock Instance;

    public int itemsRequired = 1;
    private int itemsCollected = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void CollectItem()
    {
        itemsCollected++;
        Debug.Log("Progress: " + itemsCollected + " / " + itemsRequired);

        if (itemsCollected >= itemsRequired)
            OpenDoor();
    }

    void OpenDoor()
    {
        gameObject.SetActive(false); // door vanishes, path opens

        if (GameManager.Instance != null)
            GameManager.Instance.EndGame(true); // solving the puzzle = escape ending, "you did it"
    }
}