using UnityEngine;

public class UIUnitInfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject unitNamePanel;
    [SerializeField] private GameObject unitCountPanel;

    public void ToggleMultiple(bool multiple)
    {
        unitNamePanel.SetActive(!multiple);
        unitCountPanel.SetActive(multiple);
    }
}
