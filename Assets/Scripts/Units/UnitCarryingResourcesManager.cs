using UnityEngine;

namespace Units
{
    public class UnitCarryingResourcesManager : MonoBehaviour
    {
        [SerializeField] private Transform resourceHolder;
    
        public void WearBackpack(BackpackType backpackType)
        {
            foreach (Transform child in resourceHolder)
            {
                child.gameObject.SetActive(backpackType.ToString() == child.name);
            }
        }

        public void TakeOffBackpack()
        {
            foreach (Transform child in resourceHolder)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
