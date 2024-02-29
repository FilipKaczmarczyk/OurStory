using TMPro;
using UnityEngine;

public class UIWoodPanel : MonoBehaviour
{
    private int _woodAmount;
    
    private void OnEnable()
    {
        UnitGathering.UpdateWoodAmount += ChangeWoodAmount;
    }

    private void OnDisable()
    {
        UnitGathering.UpdateWoodAmount -= ChangeWoodAmount;
    }


    [SerializeField] private TextMeshProUGUI woodText;

    private void ChangeWoodAmount(int woodToAdd)
    {
        _woodAmount += woodToAdd;
        
        UpdateText();
    }
    
    private void UpdateText()
    {
        woodText.SetText(_woodAmount.ToString());
    }
}
