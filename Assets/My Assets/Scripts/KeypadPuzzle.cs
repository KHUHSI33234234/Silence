
using UnityEngine;
using TMPro;

public class KeypadPuzzle : MonoBehaviour
{
    [Header("Keypad Settings")]
    public string correctCode = "4826";
    public GameObject keypadPanel;
    public TMP_Text codeDisplay;
    public GameObject interactionPrompt;
    public DoorController secondDoor;

    private string enteredCode = "";
    private bool playerNearby = false;
    private bool unlocked = false;

    void Start()
    {
        keypadPanel.SetActive(false);
        interactionPrompt.SetActive(false);
    }

    void Update()
    {
        if (playerNearby && !unlocked && Input.GetKeyDown(KeyCode.E))
        {
            interactionPrompt.SetActive(false);
            OpenKeypad();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (!unlocked)
                interactionPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            interactionPrompt.SetActive(false);

            if (keypadPanel.activeSelf)
                CloseKeypad();
        }
    }

    public void OpenKeypad()
    {
        if (unlocked) return;

        keypadPanel.SetActive(true);
        enteredCode = "";
        UpdateDisplay();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void AddDigit(string digit)
    {
        if (unlocked || enteredCode.Length >= 4) return;

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

    public void SubmitCode()
    {
        if (unlocked) return;

        if (enteredCode == correctCode)
        {
            unlocked = true;
            Debug.Log("Correct code! Second puzzle completed.");

            keypadPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (secondDoor != null)
            {
                secondDoor.OpenDoor(() =>
                {
                    if (GameManager.Instance != null)
                        GameManager.Instance.CompletePuzzle(2);
                });
            }
            else
            {
                Debug.LogError("Second Door has not been assigned!");
            }
        }
        else
        {
            Debug.Log("Incorrect code!");
            codeDisplay.text = "INCORRECT";
            enteredCode = "";
        }
    }

    public void CloseKeypad()
    {
        keypadPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerNearby && !unlocked)
            interactionPrompt.SetActive(true);
    }
}
