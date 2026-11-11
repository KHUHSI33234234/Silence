
using UnityEngine;
using System.Collections;

public class CanvasAutoDisable : MonoBehaviour
{
    public Canvas targetCanvas;
    public float displayDuration = 5f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(displayDuration);

        if (targetCanvas != null)
        {
            targetCanvas.enabled = false;
        }
    }
}
