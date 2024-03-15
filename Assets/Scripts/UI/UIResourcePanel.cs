using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIResourcePanel : MonoBehaviour
    {
        [SerializeField] private ResourceSO resource;
        [SerializeField] private TextMeshProUGUI resourceCountText;
        
        private int _resourceAmount;
    
        private void OnEnable()
        {
            resource.updateResourceCount.AddListener(UpdateText);
        }

        private void OnDisable()
        {
            resource.updateResourceCount.RemoveListener(UpdateText);
        }
    
        private void UpdateText(int amount)
        {
            resourceCountText.SetText(amount.ToString());
        }
    }
}
