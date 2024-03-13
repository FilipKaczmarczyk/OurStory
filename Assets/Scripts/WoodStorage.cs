using UnityEngine;

public class WoodStorage : MonoBehaviour
{
    [SerializeField] private Transform storePoint;
    private void Awake()
    {
        StorageManager.WoodStorages.Add(this);
    }

    public Vector3 GetStorePoint()
    {
        return storePoint.position;
    }
}
