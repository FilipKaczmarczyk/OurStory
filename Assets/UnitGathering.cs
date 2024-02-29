using System;
using UnityEngine;

public class UnitGathering : MonoBehaviour
{
    private enum State
    {
        Idle,
        MovingToResource,
        GatheringResource,
        MovingToStorage,
        StoreResource
    }

    public static Action<int> UpdateWoodAmount;
    
    private State _currentState;
    
    private MovableUnit _movableUnit;
    private Resource _targetResource;
    private WoodStorage _woodStorage;

    private int _inventoryWoodAmount;
    private int _maxCarryingWoodAmount = 3;
    
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
                
                if (!_targetResource) return;
                _currentState = State.MovingToResource;
                break;
            
            case State.MovingToResource:
                
                _movableUnit.Move(_targetResource.GetPosition(), 1.2f, () =>
                {
                    _currentState = State.GatheringResource;
                });
                break;
            
            case State.GatheringResource:
                
                if (_inventoryWoodAmount >= _maxCarryingWoodAmount)
                {
                    _woodStorage = StorageManager.GetClosedWoodStorage(transform);
                    _currentState = State.MovingToStorage;
                }
                else
                {
                    _movableUnit.PlayAnimation(() => 
                    {
                        _inventoryWoodAmount++;
                    });
                }
                
                break;
            
            case State.MovingToStorage:
                
                _movableUnit.ChangeHoldingItem(Item.ItemType.Wood);
                _movableUnit.Move(_woodStorage.transform.position, 2f, () =>
                {
                    _currentState = State.StoreResource;
                });
                
                break;

            case State.StoreResource:

                UpdateWoodAmount?.Invoke(_inventoryWoodAmount);
                _inventoryWoodAmount = 0;
                _movableUnit.ChangeHoldingItem(Item.ItemType.Axe);
                _currentState = State.Idle;
                
                break;
                
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetTargetResource(Resource targetResource)
    {
        _currentState = State.Idle;
        _targetResource = targetResource;
    }
}
