using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class SelectableUnit : MonoBehaviour, ISelectable
    {
        [SerializeField] private Image selectionCircle;

        public bool IsSelected { get; private set; }
    
        protected virtual void Awake()
        {
            SelectionManager.AvailableUnits.Add(this);
        }
    
        private void OnDestroy()
        {
            SelectionManager.AvailableUnits.Remove(this);
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

        public string GetName()
        {
            return "Unit";
        }

        public void ShowOnUI()
        {
            throw new System.NotImplementedException();
        }
    }
}
