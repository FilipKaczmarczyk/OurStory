using Buildings;
using UnityEngine;

namespace UI
{
    public class UIBuildingButton : MonoBehaviour
    {
        [SerializeField] private BuildingBlueprint buildingBlueprint;
    
        public void SpawnBlueprint()
        {
            Instantiate(buildingBlueprint);
        }
    }
}
