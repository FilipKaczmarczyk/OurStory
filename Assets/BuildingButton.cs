using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] private BuildingBlueprint buildingBlueprint;
    
    public void SpawnBlueprint()
    {
        Instantiate(buildingBlueprint);
    }
}
