using UnityEngine;

public class HoverHighlight : MonoBehaviour
{
    public Material hoverMaterial;
    private Material[] originalMaterials;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterials = objectRenderer.materials;
        }
    }

    public void ApplyHoverMaterial()
    {
        if (objectRenderer != null && hoverMaterial != null)
        {
            Material[] newMaterials = new Material[originalMaterials.Length + 1];
            originalMaterials.CopyTo(newMaterials, 0);
            newMaterials[newMaterials.Length - 1] = hoverMaterial;

            objectRenderer.materials = newMaterials;
        }
    }

    public void RemoveHoverMaterial()
    {
        if (objectRenderer != null)
        {
            objectRenderer.materials = originalMaterials;
        }
    }
}