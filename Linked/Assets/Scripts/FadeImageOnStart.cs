using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeImageOnStart : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;

    void Start()
    {
        fadeImage.color = Color.white;
        
        fadeImage.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad);
    }
}