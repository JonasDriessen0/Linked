using UnityEngine;

public class FlashColor : MonoBehaviour
{
    [Header("Flash Settings")]
    public Color targetColor = Color.red;  // The color to flash to
    public float flashDuration = 1f;       // Duration to take to change the color
    public float flashInterval = 2f;       // Interval between flashes

    private Material objectMaterial;
    private Color originalColor;
    private bool isFlashing = false;

    void Start()
    {
        // Get the object's material and save its original color
        objectMaterial = GetComponent<Renderer>().material;
        originalColor = objectMaterial.color;

        // Start the flashing loop
        StartCoroutine(FlashLoop());
    }

    private System.Collections.IEnumerator FlashLoop()
    {
        while (true)
        {
            // Flash the color
            yield return StartCoroutine(FlashToTargetColor());
            // Wait for the interval before starting the next flash
            yield return new WaitForSeconds(flashInterval);
        }
    }

    private System.Collections.IEnumerator FlashToTargetColor()
    {
        float flashTimer = 0f;
        while (flashTimer < flashDuration)
        {
            flashTimer += Time.deltaTime;
            float lerpValue = Mathf.Clamp01(flashTimer / flashDuration);
            objectMaterial.color = Color.Lerp(originalColor, targetColor, lerpValue);
            yield return null;
        }

        // After the flash duration, reset the color smoothly
        float resetTime = 0.5f;
        float resetTimer = 0f;

        while (resetTimer < resetTime)
        {
            resetTimer += Time.deltaTime;
            float lerpValue = Mathf.Clamp01(resetTimer / resetTime);
            objectMaterial.color = Color.Lerp(targetColor, originalColor, lerpValue);
            yield return null;
        }
    }
}