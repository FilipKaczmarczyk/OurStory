using GameInput;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MovableUnit : SelectableUnit
{
    private NavMeshAgent _navMeshAgent;
    private Camera _camera;
    private RaycastHit _hit;
    private LayerMask _layerMask;
    
    protected override void Awake()
    {
        base.Awake();

        _camera = Camera.main;
        _layerMask = LayerMask.GetMask("Ground");
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void OnEnable()
    {
        InputReader.ActionButtonEvent += HandleAction;
    }
    
    private void OnDisable()
    {
        InputReader.ActionButtonEvent -= HandleAction;
    }

    private void HandleAction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _layerMask))
            {
                Move(_hit.point);
            }
        }
    }

    private void Move(Vector3 destination)
    {
        if (!_selected)
            return;
        
        _navMeshAgent.SetDestination(destination);
    }
}
