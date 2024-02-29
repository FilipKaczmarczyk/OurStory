using UnityEngine;
using UnityEngine.UI;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField] private Image selectionCircle;

    public bool IsSelected { get; private set; }
    
    protected virtual void Awake()
    {
        UnitSelection.AvailableUnits.Add(this);
    }
    
    private void OnDestroy()
    {
        UnitSelection.AvailableUnits.Remove(this);
    }

    public void Select()
    {
        IsSelected = true;
        selectionCircle.enabled = true;
    }
    
    public void Deselect()
    {
        IsSelected = false;
        selectionCircle.enabled = false;
    }
}
