using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private ResourceSO resource;
    [SerializeField] private float gatheringDistance;
    [SerializeField] private int hitsNeededToGain;
    [SerializeField] private int resourceStackCount;
    
    public float GetGatheringDistance()
    {
        return gatheringDistance;
    }

    public int GetHitsNeededToGain()
    {
        return hitsNeededToGain;
    }

    public int GetResourceStackCount()
    {
        return resourceStackCount;
    }
    
    public ResourceSO GetResource()
    {
        return resource;
    }
    
    public WeaponType GetGatheringTool()
    {
        return resource.gatheringTool;
    }

    public BackpackType GetRawMaterial()
    {
        return resource.rawMaterial;
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, gatheringDistance);
    }
}