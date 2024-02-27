using UnityEngine;
using UnityEngine.UI;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField] private Image selectionCircle;

    protected bool _selected;
    
    protected virtual void Awake()
    {
        UnitSelection.AvailableUnits.Add(this);
    }

    public void Select()
    {
        _selected = true;
        selectionCircle.enabled = true;
    }
    
    public void Deselect()
    {
        _selected = false;
        selectionCircle.enabled = false;
    }
}
