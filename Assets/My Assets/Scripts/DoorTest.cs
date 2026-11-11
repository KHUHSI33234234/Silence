
using UnityEngine;

public class DoorTest : MonoBehaviour
{
    public DoorController door;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            door.OpenDoor();
        }
    }
}
