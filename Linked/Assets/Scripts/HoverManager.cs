using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask hoverLayerMask;
    public HoverHighlight lastHoveredObject;

    void Update()
    {
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
                    Debug.Log("Hovering over: " + hitObject.name);
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
}