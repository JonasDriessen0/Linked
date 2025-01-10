using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FallDetector : MonoBehaviour
{
    public Transform spawnPoint;
    public Image fadeImage;
    public MouseLook mouseLook;
    public PlayerMovement playerMovement;
    public float fadeDuration = 2f;

    private CharacterController characterController;

    private void Start()
    {
        characterController = playerMovement.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartDeathSequence(other.gameObject);
        }
    }

    private void StartDeathSequence(GameObject player)
    {
        mouseLook.enabled = false;
        
        Camera.main.transform.DOLookAt(player.transform.position, fadeDuration).SetEase(Ease.OutQuad);
        
        fadeImage.DOFade(1f, fadeDuration).OnComplete(() => RespawnPlayer(player));
    }

    private void RespawnPlayer(GameObject player)
    {
        characterController.enabled = false;
        
        player.transform.position = spawnPoint.position;
        
        playerMovement.ResetVelocity();
        
        characterController.enabled = true;
        
        Camera.main.transform.position = mouseLook.transform.position;
        Camera.main.transform.rotation = mouseLook.transform.rotation;
        
        mouseLook.enabled = true;
        
        fadeImage.DOFade(0f, fadeDuration);
    }
}