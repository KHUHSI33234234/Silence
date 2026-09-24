using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject endScreenPanel;
    public TMP_Text endScreenText;

    public string trappedEndingText = "You were stuck in a loop and never escaped.\nIsolation turned to anger, and anger kept you trapped.";
    public string escapeEndingText = "You walked through the isolation instead of raging against it.\nYou did it.";

    private bool gameEnded = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void EndGame(bool escaped)
    {
        if (gameEnded) return;
        gameEnded = true;

        if (endScreenText != null)
            endScreenText.text = escaped ? escapeEndingText : trappedEndingText;

        if (endScreenPanel != null)
            endScreenPanel.SetActive(true);

        // Unlock the mouse so the player can click the Restart button
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        // Lock the mouse again when gameplay restarts
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}