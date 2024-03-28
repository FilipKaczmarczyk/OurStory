using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class SelectableUnit : MonoBehaviour, ISelectable
    {
        [SerializeField] private UnitSO unit;
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

        public string GetUnitType()
        {
            return unit.type;
        }

        public string GetUnitName()
        {
            return unit.firstName;
        }
        
        public int GetUnitDamage()
        {
            return unit.damage;
        }
        
        public int GetUnitArmour()
        {
            return unit.armour;
        }
        
        public int GetUnitHealth()
        {
            return unit.healthPoint;
        }

        public Sprite GetUnitSprite()
        {
            return unit.sprite;
        }
    }
}
