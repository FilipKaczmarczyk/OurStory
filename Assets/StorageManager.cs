using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public static readonly HashSet<WoodStorage> WoodStorages = new HashSet<WoodStorage>();

    public static WoodStorage GetClosedWoodStorage(Transform targetPosition)
    {
        WoodStorage closedWoodStorage = null;
        
        foreach (var woodStorage in WoodStorages)
        {
            if (closedWoodStorage != null)
            {
                if ((woodStorage.transform.position - targetPosition.position).magnitude < (closedWoodStorage.transform.position - targetPosition.position).magnitude)
                    closedWoodStorage = woodStorage;
            }
            else
            {
                closedWoodStorage = woodStorage;
            }
            
        }

        return closedWoodStorage;
    }
}
