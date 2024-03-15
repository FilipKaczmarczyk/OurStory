using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "ScriptableObjects/New Resource")]
    public class ResourceSO : ScriptableObject
    {
        public UnityEvent<int> updateResourceCount = new UnityEvent<int>();
    
        [Header("Gathering")]
        public WeaponType gatheringTool;
        public BackpackType rawMaterial;

        private int _resourceCount;

        public void AddResource(int resourceToAdd)
        {
            _resourceCount += resourceToAdd;

            updateResourceCount.Invoke(_resourceCount);
        }
    }
}


