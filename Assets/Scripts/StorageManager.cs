using System.Collections.Generic;
using Buildings;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public static readonly HashSet<Storage> Storages = new HashSet<Storage>();

    public static Storage GetClosedStorage(Transform targetPosition)
    {
        Storage closedStorage = null;
        
        foreach (var storage in Storages)
        {
            if (closedStorage != null)
            {
                if ((storage.transform.position - targetPosition.position).magnitude < (closedStorage.transform.position - targetPosition.position).magnitude)
                    closedStorage = storage;
            }
            else
            {
                closedStorage = storage;
            }
            
        }

        return closedStorage;
    }
}
