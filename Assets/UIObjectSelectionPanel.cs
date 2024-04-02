using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectSelectionPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unitTypeText;
    [SerializeField] private Image objectImage;
    
    public void SetObjectSelectionPanel(ISelectable selectedObject)
    {
        SetObjectInfoPanel(selectedObject);
    }

    private void SetObjectInfoPanel(ISelectable selectedObject)
    {
        unitTypeText.text = selectedObject.GetObjectType();
        objectImage.sprite = selectedObject.GetObjectSprite();
    }
}
