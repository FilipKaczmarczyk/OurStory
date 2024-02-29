using UnityEngine;

public class WoodStorage : MonoBehaviour
{
    private void Awake()
    {
        StorageManager.WoodStorages.Add(this);
    }
}
