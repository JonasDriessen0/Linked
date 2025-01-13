using UnityEngine;

public class HoverManager : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask hoverLayerMask;
    public HoverHighlight lastHoveredObject;
    public OverseerController overseerController;
    private HoverHighlight selectedObject;
    private bool isObjectSelected;

    void Update()
    {
        HandleHover();
        HandleSelection();
    }

    void HandleHover()
    {
        if (isObjectSelected) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hoverLayerMask))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("MovableObject"))
            {
                HoverHighlight hoverHighlight = hitObject.GetComponent<HoverHighlight>();

                if (lastHoveredObject != null && lastHoveredObject != hoverHighlight)
                {
                    lastHoveredObject.RemoveHoverMaterial();
                }

                if (hoverHighlight != null && hoverHighlight != lastHoveredObject)
                {
                    hoverHighlight.ApplyHoverMaterial();
                    lastHoveredObject = hoverHighlight;
                }
            }
            else
            {
                if (lastHoveredObject != null)
                {
                    lastHoveredObject.RemoveHoverMaterial();
                    lastHoveredObject = null;
                }
            }
        }
    }

    void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isObjectSelected)
            {
                DeselectObject();
            }
            else if (lastHoveredObject != null)
            {
                SelectObject(lastHoveredObject);
            }
        }
    }

    void SelectObject(HoverHighlight hoverHighlight)
    {
        selectedObject = hoverHighlight;
        isObjectSelected = true;
        Cursor.visible = false;
        overseerController.enabled = false;
        selectedObject.GetComponent<MovableObject>().StartMoving();
    }

    public void DeselectObject()
    {
        if (selectedObject != null)
        {
            selectedObject.RemoveHoverMaterial();
            selectedObject.GetComponent<MovableObject>().StopMoving();
        }
        selectedObject = null;
        isObjectSelected = false;
        Cursor.visible = true;
        overseerController.enabled = true;
    }
}
