using UnityEngine;

namespace Units
{
    public class UnitHoldingWeaponManager : MonoBehaviour
    {
        [SerializeField] private Transform slotTransform;

        public void ChangeHoldingItem(WeaponType weaponType)
        {
            foreach (Transform child in slotTransform)
            {
                child.gameObject.SetActive(weaponType.ToString() == child.name);
            }
        }
    }
}
