
using UnityEngine;
using TMPro;

public class KeypadPuzzle : MonoBehaviour
{
    public string correctCode = "4826";
    public GameObject keypadPanel;
    public TMP_Text codeDisplay;
    public DoorController secondDoor;

    private string enteredCode = "";
    private bool playerNearby = false;
    private bool unlocked = false;

    void Start()
    {
        keypadPanel.SetActive(false);
    }

    void Update()
    {
        if (playerNearby && !unlocked && Input.GetKeyDown(KeyCode.E))
        {
            keypadPanel.SetActive(true);
            enteredCode = "";
            UpdateDisplay();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            keypadPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void AddDigit(string digit)
    {
        if (enteredCode.Length >= 4 || unlocked) return;

        enteredCode += digit;
        UpdateDisplay();
    }

    public void ClearCode()
    {
        enteredCode = "";
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        codeDisplay.text = enteredCode;
    }

    public void CloseKeypad()
    {
        keypadPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SubmitCode()
    {
        if (enteredCode == correctCode)
        {
            unlocked = true;
            keypadPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            secondDoor.OpenDoor(() =>
            {
                GameManager.Instance.CompletePuzzle(2);
            });
        }
        else
        {
            codeDisplay.text = "INCORRECT";
            enteredCode = "";
        }
    }
}
