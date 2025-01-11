using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class FadeAndSwitchScene : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartFadeAndSwitch();
        }
    }

    private void StartFadeAndSwitch()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(1f, 1f, 1f, 0f);
        
        fadeImage.DOFade(1f, fadeDuration).OnComplete(SwitchToNextScene);
    }

    private void SwitchToNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load. This is the last scene.");
        }
    }
}