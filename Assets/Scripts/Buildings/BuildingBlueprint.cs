using System.Collections.Generic;
using GameInput;
using UnityEngine;

namespace Buildings
{
    public class BuildingBlueprint : MonoBehaviour
    {
        [SerializeField] private GameObject building;
        [SerializeField] private BuildingBlueprintVisual blueprintVisual;

        private Camera _camera;

        private readonly HashSet<Collider> _overlappingColliders = new();
        private RaycastHit _hit;

        private LayerMask _buildingLayerMask;
    
        private bool _isReadyToBuild = true;
        private bool IsReadyToBuild
        { 
            set
            {
                if (_isReadyToBuild == value)
                    return;

                _isReadyToBuild = value;
                blueprintVisual.UpdateColor(value);
            }
        }
    
        private void Awake()
        {
            _buildingLayerMask = LayerMask.GetMask("Ground");
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            InputReader.BuildConfirmEvent += TryBuild;
        }

        private void OnDisable()
        {
            InputReader.BuildConfirmEvent -= TryBuild;
        }

        private void TryBuild()
        {
            if (!_isReadyToBuild)
                return;
        
            BuildFromBlueprint();
        
            Destroy(gameObject);
        }
    
        private void Update()
        {
            UpdatePositionToCursor();
        }

        private void UpdatePositionToCursor()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _buildingLayerMask))
            {
                transform.position = _hit.point;
            }
        }
    
        private void BuildFromBlueprint()
        {
            var transformCached = transform;
            Instantiate(building, transformCached.position, transformCached.rotation);
        }

        private void OnTriggerEnter(Collider other)
        {
            _overlappingColliders.Add(other);

            IsReadyToBuild = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_overlappingColliders.Contains(other)) return;
        
            _overlappingColliders.Remove(other);
            IsReadyToBuild = _overlappingColliders.Count == 0;
        }
    }
}
