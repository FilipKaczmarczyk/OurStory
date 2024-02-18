using UnityEngine;

public class BuildingBlueprintVisual : MonoBehaviour
{
    private Material _material;
    
    private void Awake()
    {
        _material = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
    }

    public void UpdateColor(Color newColor)
    {
        _material.color = newColor;
    }
}
