using UnityEngine;

public class FlashColor : MonoBehaviour
{
    [Header("Flash Settings")]
    public Color targetColor = Color.red;
    public float flashDuration = 1f;
    public float flashInterval = 2f;

    private Material objectMaterial;
    private Color originalColor;
    private bool isFlashing = false;

    void Start()
    {
        objectMaterial = GetComponent<Renderer>().material;
        originalColor = objectMaterial.color;
        
        StartCoroutine(FlashLoop());
    }

    private System.Collections.IEnumerator FlashLoop()
    {
        while (true)
        {
            yield return StartCoroutine(FlashToTargetColor());
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