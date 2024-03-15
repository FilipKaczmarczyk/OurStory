using System;
using ProjectDawn.Navigation.Hybrid;
using ProjectDawn.Navigation.Sample.Zerg;
using UnityEngine;

namespace Units
{
    public class MovableUnit : SelectableUnit
    {
        private enum State
        {
            Idle,
            Move,
            Animation,
        }

        [SerializeField] private Animator animator;
        private UnitBrainSystem _unitBrainSystem;
    
        private AgentAuthoring _agentAuthoring;
        private UnitHoldingWeaponManager _unitHoldingWeaponManager;
        private UnitCarryingResourcesManager _unitCarryingResourcesManager;
    
        private static readonly int Moving = Animator.StringToHash("moving");
        private static readonly int Attack = Animator.StringToHash("attack");
    
        private float _arrivalDistance;

        private Vector3 _targetPosition;
        private Action _nextAction;

        private State _currentState;

        private bool _gathering;
        private AgentSmartStopAuthoring _agentSmartStop;
    
        protected override void Awake()
        {
            base.Awake();

            _agentSmartStop = GetComponent<AgentSmartStopAuthoring>();
            _agentAuthoring = GetComponent<AgentAuthoring>();
            _unitHoldingWeaponManager = GetComponent<UnitHoldingWeaponManager>();
            _unitCarryingResourcesManager = GetComponent<UnitCarryingResourcesManager>();
        }

        private void Start()
        {
            _currentState = State.Idle;
        }

        private void Update()
        {
            if (_currentState != State.Move)
                return;
        
            var v1XZ = new Vector2(transform.position.x, transform.position.z);
            var v2XZ = new Vector2(_targetPosition.x, _targetPosition.z);

            var distancesq = Vector2.Distance(v1XZ, v2XZ);
        
            if (_gathering)
            {
                if (distancesq <= _arrivalDistance)
                {
                    var q = Quaternion.LookRotation(_targetPosition - transform.position);
                    transform.rotation = q;
                
                    _agentAuthoring.Stop();
                    animator.SetBool(Moving, false);
                    _currentState = State.Idle;

                    _nextAction?.Invoke();
                }
            }
            else
            {
                if (_agentAuthoring.IsStopped())
                {
                    _agentAuthoring.Stop();
                    animator.SetBool(Moving, false);
                    _currentState = State.Idle;

                    _nextAction?.Invoke();
                }
            }
        }

        public void Move(Vector3 destination, float arrivalDistance = 0.1f, Action callbackAction = null, bool gathering = false)
        {
            _gathering = gathering;
            _agentSmartStop.enabled = !gathering;
        
            _arrivalDistance = arrivalDistance;
            _targetPosition = destination;
            animator.SetBool(Moving, true);

            _agentAuthoring.SetDestination(destination);
            _currentState = State.Move;
            _nextAction = callbackAction;
        }

        public void PlayAnimation(Action callbackAction = null)
        {
            animator.SetTrigger(Attack);
            _currentState = State.Animation;
            _nextAction = callbackAction;
        }
    
        public void EndAnimation()
        {
            _currentState = State.Idle;
        
            _nextAction?.Invoke();
        }

        public void ChangeHoldingItem(WeaponType weaponType)
        {
            _unitHoldingWeaponManager.ChangeHoldingItem(weaponType);
        }

        public void PickMaterial(BackpackType backpackType)
        {
            _unitCarryingResourcesManager.WearBackpack(backpackType);
        }

        public void DropMaterial()
        {
            _unitCarryingResourcesManager.TakeOffBackpack();
        }
    
        public bool IsIdle()
        {
            return _currentState == State.Idle;
        }
    }
}
