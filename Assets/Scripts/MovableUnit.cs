using System;
using UnityEngine;
using UnityEngine.AI;

public class MovableUnit : SelectableUnit
{
    private enum State
    {
        Idle,
        Move,
        Animation,
    }
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private UnitHoldingItemManager _unitHoldingItemManager;
    
    private static readonly int Moving = Animator.StringToHash("isMoving");
    private static readonly int Cut = Animator.StringToHash("Cut");
    private static readonly int HoldingResources = Animator.StringToHash("HoldingResources");
    
    private float _arrivalDistance = 1.2f;

    private Vector3 _targetPosition;
    private Action _nextAction;

    private State _currentState;

    private bool _holdingResources;
    
    protected override void Awake()
    {
        base.Awake();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _unitHoldingItemManager = GetComponent<UnitHoldingItemManager>();
    }

    private void Start()
    {
        _currentState = State.Idle;
    }

    private void Update()
    {
        if (_currentState != State.Move)
            return;

        Debug.Log((transform.position - _targetPosition).magnitude);
        
        if ((transform.position - _targetPosition).magnitude <= _arrivalDistance)
        {
            _navMeshAgent.ResetPath();
            _animator.SetBool(Moving, false);
            _currentState = State.Idle;

            _nextAction?.Invoke();
        }
    }

    public void Move(Vector3 destination, float arrivalDistance = 0.1f, Action callbackAction = null)
    {
        _arrivalDistance = arrivalDistance;
        _targetPosition = destination;
        _animator.SetBool(Moving, true);

        _animator.SetBool(HoldingResources, _holdingResources);

        _navMeshAgent.SetDestination(destination);
        _currentState = State.Move;
        _nextAction = callbackAction;
    }

    public void PlayAnimation(Action callbackAction = null)
    {
        _animator.SetTrigger(Cut);
        _currentState = State.Animation;
        _nextAction = callbackAction;
    }
    
    public void EndAnimation()
    {
        _currentState = State.Idle;
        
        _nextAction?.Invoke();
    }

    public void ChangeHoldingItem(Item.ItemType itemType)
    {
        _unitHoldingItemManager.ChangeHoldingItem(itemType);

        _holdingResources = itemType == Item.ItemType.Wood;
    }
    
    public bool IsIdle()
    {
        return _currentState == State.Idle;
    }
}
