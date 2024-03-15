using System;
using Buildings;
using Resources;
using UnityEngine;

namespace Units
{
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

        private State _currentState;
    
        private MovableUnit _movableUnit;
        private ResourceNode _currentTargetResourceNode;
        private Storage _storage;

        private int _hitsCount;
    
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
                
                    if (!_currentTargetResourceNode) return;
                    _currentState = State.MovingToResource;
                    break;
            
                case State.MovingToResource:
                
                    _movableUnit.Move(_currentTargetResourceNode.transform.position, _currentTargetResourceNode.GetGatheringDistance(), () =>
                    {
                        _currentState = State.GatheringResource;
                    }, true);
                    break;
            
                case State.GatheringResource:
                
                    if (_hitsCount >= _currentTargetResourceNode.GetHitsNeededToGain())
                    {
                        _storage = StorageManager.GetClosedStorage(transform);
                        _currentState = State.MovingToStorage;
                    }
                    else
                    {
                        _movableUnit.PlayAnimation(() => 
                        {
                            _hitsCount++;
                        });
                    }
                
                    break;
            
                case State.MovingToStorage:
                
                    _movableUnit.PickMaterial(_currentTargetResourceNode.GetRawMaterial());
                    _movableUnit.Move(_storage.GetStorePoint(), 0.5f, () =>
                    {
                        _currentState = State.StoreResource;
                    }, true);
                
                    break;

                case State.StoreResource:

                    _currentTargetResourceNode.GetResource().AddResource(_currentTargetResourceNode.GetResourceStackCount());
                    _hitsCount = 0;
                    _movableUnit.DropMaterial();
                    _currentState = State.Idle;
                
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetTargetResource(ResourceNode targetResourceNode)
        {
            _currentState = State.Idle;

            if (_currentTargetResourceNode != null)
            {
                if (_currentTargetResourceNode.GetResource() != targetResourceNode.GetResource())
                {
                    _movableUnit.ChangeHoldingItem(targetResourceNode.GetGatheringTool());
                }
                else
                {
                    // To DO
                }
            }
            else
            {
                _movableUnit.ChangeHoldingItem(targetResourceNode.GetGatheringTool());
            }
        
            _currentTargetResourceNode = targetResourceNode;
        }
    }
}
