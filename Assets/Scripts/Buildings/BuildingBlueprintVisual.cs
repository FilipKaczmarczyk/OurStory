using UnityEngine;

namespace Buildings
{
    public class BuildingBlueprintVisual : MonoBehaviour
    {
        private Material _material;
    
        private readonly Color _readyToBuildColor = new Color(0, 1, 0, 0.5f);
        private readonly Color _notReadyToBuildColor = new Color(1, 0, 0, 0.5f);
    
        private void Awake()
        {
            _material = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        }

        public void UpdateColor(bool isReadyToBuild)
        {
            _material.color = isReadyToBuild ? _readyToBuildColor : _notReadyToBuildColor;
        }
    }
}
