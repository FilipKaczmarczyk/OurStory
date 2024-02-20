using UnityEngine;
using UnityEngine.UI;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField] private Image selectionCircle;

    private void Awake()
    {
        UnitSelection.AvailableUnits.Add(this);
    }

    public void Select()
    {
        selectionCircle.enabled = true;
    }
    
    public void Deselect()
    {
        selectionCircle.enabled = false;
    }
}
