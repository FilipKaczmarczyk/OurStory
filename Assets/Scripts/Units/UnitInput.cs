using Buildings;
using GameInput;
using Resources;
using UnityEngine;

namespace Units
{
    public class UnitInput : MonoBehaviour
    {
        private Camera _camera;
        private RaycastHit _hit;
        private LayerMask _groundLayerMask;
        private LayerMask _resourceLayerMask;
        private LayerMask _buildingLayerMask;

        private MovableUnit _movableUnit;
        private UnitGathering _unitGathering;
        private UnitBuilding _unitBuilding;
    
        private void Awake()
        {
            _camera = Camera.main;
        
            _groundLayerMask = LayerMask.GetMask("Ground");
            _resourceLayerMask = LayerMask.GetMask("Resource");
            _buildingLayerMask = LayerMask.GetMask("Building");

            _movableUnit = GetComponent<MovableUnit>();
            _unitGathering = GetComponent<UnitGathering>();
            _unitBuilding = GetComponent<UnitBuilding>();
        }

        private void OnEnable()
        {
            InputReader.GiveUnitOrderEvent += HandleGiveUnitOrder;
        }
    
        private void OnDisable()
        {
            InputReader.GiveUnitOrderEvent -= HandleGiveUnitOrder;
        }
    
        private void HandleGiveUnitOrder()
        {
            if (!_movableUnit.IsSelected)
                return;
        
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _buildingLayerMask))
            {
                var building = _hit.transform.GetComponent<Building>();
                
                _unitBuilding.enabled = true;
                _unitGathering.enabled = false;
                
                _unitBuilding.SetTargetBuilding(building);
            
                return;
            }
            
            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _resourceLayerMask))
            {
                var resource = _hit.transform.GetComponent<ResourceNode>();
                
                _unitGathering.enabled = true;
                _unitBuilding.enabled = false;
                
                _unitGathering.SetTargetResource(resource);
            
                return;
            }

            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _groundLayerMask))
            {
                _movableUnit.Move(_hit.point);
                _unitGathering.enabled = false;
                _unitBuilding.enabled = false;
            }
        }
    }
}
