using System.Collections.Generic;
using Units;
using UnityEngine;

public class UISelectionPanel : MonoBehaviour
{
    [SerializeField] private GameObject unitPanelHolder;
    [SerializeField] private UIUnitSelectionPanel unitSelectionPanel;
    
    [SerializeField] private GameObject objectPanelHolder;
    [SerializeField] private UIObjectSelectionPanel objectSelectionPanel;
    
    public void UpdateSelection(HashSet<SelectableUnit> selectedUnits)
    {
        if (selectedUnits.Count <= 0)
        {
            HidePanels();
            return;
        }

        ShowUnits(selectedUnits);
    }

    private void ShowUnits(HashSet<SelectableUnit> selectedUnits)
    {
        ShowUnitPanel();
        unitSelectionPanel.SetUnitsSelectionPanel(selectedUnits);
    }

    public void ShowObject(ISelectable selectable)
    {
        ShowObjectsPanel();
        objectSelectionPanel.SetObjectSelectionPanel(selectable);
    }

    private void HidePanels()
    {
        unitPanelHolder.SetActive(false);
        objectPanelHolder.SetActive(false);
    }
    
    private void ShowUnitPanel()
    {
        unitPanelHolder.SetActive(true);
        objectPanelHolder.SetActive(false);
    }

    private void ShowObjectsPanel()
    {
        objectPanelHolder.SetActive(true);
        unitPanelHolder.SetActive(false);
    }
}
