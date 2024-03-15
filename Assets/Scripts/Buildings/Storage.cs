using UnityEngine;

namespace Buildings
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private Transform storePoint;
        private void Awake()
        {
            StorageManager.Storages.Add(this);
        }

        public Vector3 GetStorePoint()
        {
            return storePoint.position;
        }
    }
}
