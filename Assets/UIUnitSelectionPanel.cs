using System.Collections.Generic;
using System.Linq;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitSelectionPanel : MonoBehaviour
{
    [SerializeField] private UIUnitInfoPanel unitInfoPanel;
    [SerializeField] private TextMeshProUGUI unitTypeText;
    [SerializeField] private TextMeshProUGUI unitNameText;
    [SerializeField] private TextMeshProUGUI unitDamageText;
    [SerializeField] private TextMeshProUGUI unitArmourText;
    [SerializeField] private TextMeshProUGUI unitHealthText;
    [SerializeField] private TextMeshProUGUI unitCountText;
    [SerializeField] private Image unitImage;
    
    public void SetUnitsSelectionPanel(HashSet<SelectableUnit> selectedUnits)
    {
        if (AreUnitsSameType(selectedUnits))
        {
            SetUnitInfoPanel(selectedUnits.First(), selectedUnits.Count);
        }
    }

    private void SetUnitInfoPanel(SelectableUnit unit, int unitCount = 1)
    {
        unitTypeText.text = unit.GetObjectType();
        unitImage.sprite = unit.GetObjectSprite();
        unitArmourText.text = unit.GetUnitArmour().ToString();
        unitDamageText.text = unit.GetUnitDamage().ToString();
        unitHealthText.text = unit.GetUnitHealth().ToString();

        unitInfoPanel.ToggleMultiple(unitCount != 1);
        
        unitNameText.text = unit.GetUnitName();
        unitCountText.text = unitCount.ToString();
    }
    
    private bool AreUnitsSameType(HashSet<SelectableUnit> selectedUnits)
    {
        var firstUnitType = selectedUnits.First().GetObjectType();
        
        foreach (var unit in selectedUnits)
        {
            if (unit.GetObjectType() != firstUnitType)
                return false;
        }

        return true;
    }

    
}
