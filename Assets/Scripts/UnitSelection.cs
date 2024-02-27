using System.Collections.Generic;
using GameInput;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelection : MonoBehaviour
{
    public static readonly HashSet<SelectableUnit> AvailableUnits = new HashSet<SelectableUnit>();
    
    [SerializeField] private RectTransform selectionBox;

    private const int MinDragDistance = 40;
    
    private bool _selectionButtonPressed;
    private bool _multiSelectionButtonPressed;
    
    private Vector3 _mouseStartPosition;
    private bool _dragSelection;

    private RaycastHit _hit;
    private Camera _camera;

    private readonly HashSet<SelectableUnit> _selectedUnits = new HashSet<SelectableUnit>();

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        InputReader.UnitSelectionButtonEvent += HandleSelection;
        InputReader.MultiSelectionButtonEvent += HandleMultiSelection;
    }
    
    private void OnDisable()
    {
        InputReader.UnitSelectionButtonEvent -= HandleSelection;
        InputReader.MultiSelectionButtonEvent -= HandleMultiSelection;
    }
    
    private void HandleSelection(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _selectionButtonPressed = true;
            
            _mouseStartPosition = Input.mousePosition;
            selectionBox.sizeDelta = Vector2.zero;
            selectionBox.gameObject.SetActive(true);
        }
        
        if (context.phase == InputActionPhase.Canceled)
        {
            _selectionButtonPressed = false;
            
            selectionBox.sizeDelta = Vector2.zero;
            selectionBox.gameObject.SetActive(false);

            if (!_dragSelection)
            {
                SelectUnitUnderMouse();
            }

            _dragSelection = false;
        }
    }

    private void HandleMultiSelection(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _multiSelectionButtonPressed = true;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            _multiSelectionButtonPressed = false;
        }
    }

    private void SelectUnitUnderMouse()
    {
        var ray = _camera.ScreenPointToRay(_mouseStartPosition);

        if (Physics.Raycast(ray, out _hit, Mathf.Infinity) && _hit.collider.TryGetComponent<SelectableUnit>(out var unit)) 
        {
            if (_multiSelectionButtonPressed)
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
                if (IsOnlySelected(unit))
                {
                    SelectAllUnits();
                }
                else
                {
                    DeselectAll();
                    SelectUnit(unit);
                }
            }
        }
        else 
        {
            DeselectAll();
        }
    }

    private bool IsOnlySelected(SelectableUnit unit)
    {
        return _selectedUnits.Contains(unit) && _selectedUnits.Count == 1;
    }
    
    private void SelectAllUnits()
    {
        foreach(var unit in AvailableUnits)
        {
            SelectUnit(unit);
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

        if ((_mouseStartPosition - Input.mousePosition).magnitude > MinDragDistance)
            _dragSelection = true;

        if (!_dragSelection)
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
            if (_multiSelectionButtonPressed) 
            {
                if (IsUnitInSelectionBox(_camera.WorldToScreenPoint(availableUnit.transform.position), bounds) && !IsSelected(availableUnit))
                {
                        SelectUnit(availableUnit);
                }
            }
            else 
            {
                if (IsUnitInSelectionBox(_camera.WorldToScreenPoint(availableUnit.transform.position), bounds))
                {
                    SelectUnit(availableUnit);
                }
                else
                {
                    DeselectUnit(availableUnit);
                }
            }
        }
    }
    
    private static bool IsUnitInSelectionBox(Vector2 position, Bounds bounds)
    {
        return position.x > bounds.min.x && position.x < bounds.max.x &&
               position.y > bounds.min.y && position.y < bounds.max.y;
    }
}
