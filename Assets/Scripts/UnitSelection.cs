using System.Collections.Generic;
using GameInput;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelection : MonoBehaviour
{
    public static readonly HashSet<SelectableUnit> AvailableUnits = new HashSet<SelectableUnit>();
    
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private float dragDelay = 0.01f;
    
    private bool _selectionButtonPressed;
    
    private Vector3 _mouseStartPosition;
    private float _mouseDownTime;

    private RaycastHit _hit;

    private readonly HashSet<SelectableUnit> _selectedUnits = new HashSet<SelectableUnit>();
    
    private HashSet<SelectableUnit> _newlySelectedUnits = new HashSet<SelectableUnit>();
    private HashSet<SelectableUnit> _deselectedUnits = new HashSet<SelectableUnit>();

    private void OnEnable()
    {
        InputReader.UnitSelectionButtonEvent += HandleSelection;
    }
    
    private void OnDisable()
    {
        InputReader.UnitSelectionButtonEvent -= HandleSelection;
    }
    
    private void HandleSelection(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _selectionButtonPressed = true;
            
            _mouseStartPosition = Input.mousePosition;
            selectionBox.sizeDelta = Vector2.zero;
            selectionBox.gameObject.SetActive(true);
            _mouseDownTime = Time.time;
        }
        
        if (context.phase == InputActionPhase.Canceled)
        {
            _selectionButtonPressed = false;
            
            selectionBox.sizeDelta = Vector2.zero;
            selectionBox.gameObject.SetActive(false);
            
            foreach (var newUnit in _newlySelectedUnits)
            {
                SelectUnit(newUnit);
            }
            
            foreach (var deselectedUnit in _deselectedUnits)
            {
                DeselectUnit(deselectedUnit);
            }
            
            _newlySelectedUnits.Clear();
            _deselectedUnits.Clear();
            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit)
                && hit.collider.TryGetComponent<SelectableUnit>(out var unit))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    if (IsSelected(unit))
                    {
                        DeselectUnit(unit);
                    }
                    else
                    {
                        SelectUnit(unit);
                    }
                }
                else
                {
                    DeselectAll();
                    SelectUnit(unit);
                }
            }
            else if (_mouseDownTime + dragDelay > Time.time)
            {
                DeselectAll();
            }

            _mouseDownTime = 0;
        }
    }

    private void SelectUnit(SelectableUnit unit)
    {
        _selectedUnits.Add(unit);
        unit.Select();
    }

    private void DeselectUnit(SelectableUnit unit)
    {
        unit.Deselect();
        _selectedUnits.Remove(unit);
    }

    private void DeselectAll()
    {
        foreach (var selectedUnit in _selectedUnits)
        {
            selectedUnit.Deselect();
        }
        
        _selectedUnits.Clear();
    }

    private bool IsSelected(SelectableUnit unit)
    {
        return _selectedUnits.Contains(unit);
    }
    
    private void Update()
    {
        if (!_selectionButtonPressed)
            return;
        
        if (_mouseDownTime + dragDelay >= Time.time)
            return;

        ResizeSelectionBox();
    }

    private void ResizeSelectionBox()
    {
        var width = Input.mousePosition.x - _mouseStartPosition.x;
        var height = Input.mousePosition.y - _mouseStartPosition.y;

        selectionBox.anchoredPosition = _mouseStartPosition + new Vector3(width / 2, height / 2);
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        var bounds = new Bounds(selectionBox.anchoredPosition, selectionBox.sizeDelta);

        foreach (var availableUnit in AvailableUnits)
        {
            if (UnitIsInSelectionBox(Camera.main.WorldToScreenPoint(availableUnit.transform.position), bounds))
            {
                if (!IsSelected(availableUnit))
                {
                    _newlySelectedUnits.Add(availableUnit);
                }

                _deselectedUnits.Remove(availableUnit);
            }
            else
            {
                _deselectedUnits.Add(availableUnit);
                _newlySelectedUnits.Remove(availableUnit);
            }
        }
    }
    
    private bool UnitIsInSelectionBox(Vector2 position, Bounds bounds)
    {
        return position.x > bounds.min.x && position.x < bounds.max.x &&
               position.y > bounds.min.y && position.y < bounds.max.y;
    }
}
