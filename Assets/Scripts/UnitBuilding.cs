using System;
using Buildings;
using Units;
using UnityEngine;

public class UnitBuilding : MonoBehaviour
{
    private enum State
    {
        Idle,
        MovingToBuilding,
        Building
    }

    private State _currentState;
    private MovableUnit _movableUnit;
    private Building _targetBuilding;
    
    private void Awake()
    {
        _movableUnit = GetComponent<MovableUnit>();
        _currentState = State.Idle;
    }
    
    private void Update()
        {
            if (!_movableUnit.IsIdle())
                return;
        
            switch (_currentState)
            {
                case State.Idle:
                
                    if (!_targetBuilding) return;
                    _currentState = State.MovingToBuilding;
                    break;
            
                case State.MovingToBuilding:
                
                    _movableUnit.Move(_targetBuilding.GetClosestPoint(transform.position), _targetBuilding.GetBuildingDistance() , () =>
                    {
                        _currentState = State.Building;
                    }, true);
                    break;
            
                case State.Building:

                    if (!_targetBuilding.GetBuildComplete())
                    {
                        _movableUnit.PlayAnimation(() => 
                        {
                            _targetBuilding.Build();
                        });
                    }
                    else
                    {
                        _targetBuilding = null;
                        _currentState = State.Idle;
                    }
                    
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    
    public void SetTargetBuilding(Building targetBuilding)
    {
        _currentState = State.Idle;
        _targetBuilding = targetBuilding;
        
        _movableUnit.ChangeHoldingItem(WeaponType.Hammer);
    }
}
