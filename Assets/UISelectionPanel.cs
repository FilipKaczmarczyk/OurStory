using System.Collections.Generic;
using System.Linq;
using TMPro;
using Units;
using UnityEngine;

public class UISelectionPanel : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private TextMeshProUGUI selectionName;
    
    public void UpdateSelection(HashSet<SelectableUnit> selectedUnits)
    {
        if (selectedUnits.Count <= 0)
        {
            Hide();
            return;
        }
        
        ShowUnits(selectedUnits);
    }

    private void ShowUnits(HashSet<SelectableUnit> selectedUnits)
    {
        SetObjectInfoPanel(selectedUnits.First(), selectedUnits.Count);
        Show();
    }
    
    public void ShowSingleObject(ISelectable selectable)
    {
        SetObjectInfoPanel(selectable);
        Show();
    }

    private void Hide()
    {
        holder.SetActive(false);
    }

    private void Show()
    {
        holder.SetActive(true);
    }

    private void SetObjectInfoPanel(ISelectable selectable, int unitsCount = 1)
    {
        selectionName.text = selectable.GetName();
        
        if (unitsCount != 1)
        {
            selectionName.text += " (" + unitsCount + ")";
        }
    }
}
