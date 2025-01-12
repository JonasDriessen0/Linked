using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Mathematics;

public class SwitchSequence : MonoBehaviour
{
    public Camera mainCamera;
    public Camera bigGuyCamera;
    public Transform bigGuyObject;
    public Transform smallGuyObject;
    public PlayerMovement playerMovement;
    public OverseerController overseerController;
    public HoverManager hoverManager;
    public MouseLook mouseLook;
    public Image fadeImage;
    public Transform overseerGameObject;
    public float lookDuration = 0.5f;
    public float zoomDuration = 2f;
    public float zoomDistance = 3f;
    public float fovIncreaseAmount = 10f;
    public float fogTransitionDuration = 1f;
    public float bigGuyFogDensity = 0.01f;
    public bool isBig;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 bigGuyoriginalPosition;
    private Quaternion bigGuyoriginalRotation;
    private float originalFOV;
    private float originalFogDensity;

    void Start()
    {
        originalPosition = mainCamera.transform.position;
        originalRotation = mainCamera.transform.rotation;
        
        bigGuyoriginalPosition = bigGuyCamera.transform.position;
        bigGuyoriginalRotation = bigGuyCamera.transform.rotation;
        originalFOV = mainCamera.fieldOfView;
        originalFogDensity = RenderSettings.fogDensity;
        fadeImage.color = new Color(1, 1, 1, 0);
    }

    public void SwitchPerspective()
    {
        if (isBig)
        {
            StartSwitchSequenceToSmall();
        }
        else
        {
            StartSwitchSequenceToBig();
        }
    }

    public void StartSwitchSequenceToBig()
    {
        isBig = true;
        playerMovement.enabled = false;
        mouseLook.enabled = false;
        
        mainCamera.transform.DOLookAt(bigGuyObject.position, lookDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            DOVirtual.DelayedCall(0.2f, () =>
            {
                Vector3 zoomPosition = bigGuyObject.position - (bigGuyObject.forward * zoomDistance);
                
                mainCamera.transform.DOMove(zoomPosition, zoomDuration).SetEase(Ease.InQuad);
                
                mainCamera.DOFieldOfView(originalFOV - fovIncreaseAmount, zoomDuration).SetEase(Ease.InQuad);
                
                fadeImage.DOFade(1f, zoomDuration).SetEase(Ease.InExpo).OnComplete(SwitchCameraToBig);
                
                DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, bigGuyFogDensity, fogTransitionDuration);
            });
        });
    }

    private void SwitchCameraToBig()
    {
        mainCamera.fieldOfView = originalFOV;
        mainCamera.enabled = false;
        bigGuyCamera.enabled = true;
        
        fadeImage.DOFade(0f, 1f).SetEase(Ease.InOutQuad);
        
        mainCamera.transform.position = originalPosition;
        mainCamera.transform.rotation = originalRotation;
        
        overseerController.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        hoverManager.enabled = true;
        overseerGameObject.gameObject.SetActive(false); 
    }

    public void StartSwitchSequenceToSmall()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isBig = false;
        playerMovement.enabled = false;
        mouseLook.enabled = false;
        if(hoverManager.lastHoveredObject != null)
            hoverManager.lastHoveredObject.RemoveHoverMaterial();
        hoverManager.enabled = false;
        
        bigGuyCamera.transform.DOLookAt(smallGuyObject.position, lookDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            DOVirtual.DelayedCall(0.2f, () =>
            {
                Vector3 zoomPosition = smallGuyObject.position;
                
                bigGuyCamera.transform.DOMove(zoomPosition, zoomDuration).SetEase(Ease.InQuad);
                
                bigGuyCamera.DOFieldOfView(originalFOV, zoomDuration).SetEase(Ease.InQuad);
                
                fadeImage.DOFade(1f, zoomDuration).SetEase(Ease.InExpo).OnComplete(SwitchCameraToSmall);
                
                DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, originalFogDensity, fogTransitionDuration);
            });
        });
    }

    private void SwitchCameraToSmall()
    {
        bigGuyCamera.fieldOfView = originalFOV;
        bigGuyCamera.enabled = false;
        mainCamera.enabled = true;
        
        fadeImage.DOFade(0f, 1f).SetEase(Ease.InOutQuad);
        
        bigGuyCamera.transform.localPosition = Vector3.zero;
        bigGuyCamera.transform.localRotation = quaternion.identity;
        
        playerMovement.enabled = true;
        mouseLook.enabled = true;
        overseerController.enabled = false;
        overseerGameObject.gameObject.SetActive(true);
    }
}
